using Domain.Common.Enums;
using Domain.Entities;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.AppTroopers.Subscription;
using Domain.Entities.LocationEntities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Infrastructure.Persistence.Services
{
    public class DbInitializer : IDbInitializer
    {

        private readonly IServiceScopeFactory _scopeFactory;
        WKTReader wktReader = new WKTReader();

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        //public static System.Data.Entity.Spatial.DbGeography CustomCreateMultiPolygon(string wktString)
        //{
        //    var sqlGeography =
        //    Microsoft.SqlServer.Types.SqlGeography.STGeomFromText(new System.Data.SqlTypes.SqlChars(wktString), 4326)
        //    .MakeValid();

        //    var invertedSqlGeography = sqlGeography.ReorientObject();
        //    if (sqlGeography.STArea() > invertedSqlGeography.STArea())
        //    {
        //        sqlGeography = invertedSqlGeography;
        //    }

        //    return System.Data.Entity.Spatial.DbSpatialServices.Default.GeographyFromProviderValue(sqlGeography);
        //}

        public static MultiPolygon MakeMultiPolygonCounterClockWise(MultiPolygon multiPolygon)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            MultiPolygon validMultiPolygon = geometryFactory.CreateMultiPolygon();


            Polygon[] polygons = new Polygon[multiPolygon.Geometries.Length];
            foreach (NetTopologySuite.Geometries.Geometry geom in multiPolygon.Geometries)
            {
                var customShape = geometryFactory.CreatePolygon(geom.Coordinates.ToArray());
                if (!customShape.Shell.IsCCW)
                {
                    customShape = (Polygon)customShape.Reverse();
                    Polygon[] polys = new Polygon[1];
                    polys[0] = customShape;
                    MultiPolygon mult = geometryFactory.CreateMultiPolygon(polys);
                    validMultiPolygon = mult;
                }
            }
            return validMultiPolygon;
        }


        public static Polygon MakePolygonCounterClockWise(Polygon polygon)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            Polygon validPolygon = geometryFactory.CreatePolygon();

            var customShape = geometryFactory.CreatePolygon(polygon.Coordinates.ToArray());
            if (!customShape.Shell.IsCCW)
            {
                customShape = (Polygon)customShape.Reverse();
                validPolygon = customShape;
            }
            return validPolygon;
        }


        public void SeedStatesandLGAs()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            //var geometryFactory = new OgcCompliantGeometryFactory();

            try
            {
                List<StateNamesandIdsVM> stateNamesandIdsList = new List<StateNamesandIdsVM>();
                using (var serviceScope = _scopeFactory.CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                    {
                        #region seed states
                        //Seed States and MultiPolygons for States
                        if (!(context.States.Any()))
                        {
                            string StatesMultiploygonsJSON = File.ReadAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"JSONDocs" + Path.DirectorySeparatorChar + "ngstatesboundaries.json"));

                            //save JSON string for state
                            var SatesJSONDemographic = new DemographicEntitiesCoordinatesJSON
                            {
                                DemographicType = "State",
                                JsonString = StatesMultiploygonsJSON
                            };
                            //context.demographicEntitiesCoordinatesJSONs.Add(SatesJSONDemographic);

                            List<StatesMultiPolygons> statesMultipolygons = JsonConvert.DeserializeObject<List<StatesMultiPolygons>>(StatesMultiploygonsJSON);

                            foreach (var state in statesMultipolygons)
                            {
                                MultiPolygon multiPolygon = (MultiPolygon)wktReader.Read(state.WKT);

                                var reversedmultiPolygon = MakeMultiPolygonCounterClockWise(multiPolygon);

                                var stateEntity = new State
                                {
                                    Id = Guid.NewGuid(),
                                    Name = state.admin1Name,
                                    Boundary = reversedmultiPolygon,
                                    shapeArea = (decimal)state.Shape_Area,
                                    shapeLength = (decimal)state.Shape_Leng,
                                    Created = DateTime.UtcNow.AddHours(1)
                                };
                                context.States.Add(stateEntity);
                                

                                StateNamesandIdsVM nameIdPair = new StateNamesandIdsVM
                                {
                                    stateId = stateEntity.Id.ToString(),
                                    stateName = stateEntity.Name
                                };

                                stateNamesandIdsList.Add(nameIdPair);
                            }

                            context.SaveChanges();
                        }

                        #endregion seed states
                        #region seed LGAs
                        if (!(context.LGAs.Any()))
                        {
                            //get all state names and Ids
                            if (!stateNamesandIdsList.Any())
                            {
                                stateNamesandIdsList =
                                    (from s in context.States
                                     select new StateNamesandIdsVM
                                     {
                                         stateId = s.Id.ToString(),
                                         stateName = s.Name
                                     }).ToList();
                            }

                            string lgaPolygonsJSON = File.ReadAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"JSONDocs" + Path.DirectorySeparatorChar + "towns_in_nigeria.json"));

                            //save JSON string for lga
                            var lgaJSONDemographic = new DemographicEntitiesCoordinatesJSON
                            {
                                DemographicType = "LGA",
                                JsonString = lgaPolygonsJSON
                            };
                            context.demographicEntitiesCoordinatesJSONs.Add(lgaJSONDemographic);


                            LGAPolygons lgasPolygons = JsonConvert.DeserializeObject<LGAPolygons>(lgaPolygonsJSON);

                            foreach (var lga in lgasPolygons.features)
                            {
                                if (lga.geometry.type == "MultiPolygon")
                                {
                                    var geoArrayList = lga.geometry.coordinates.ToObject<object[][][]>();
                                    Polygon[] polygons = new Polygon[geoArrayList.Length];//list of polygons

                                    for (int i = 0; i < geoArrayList.Length; i++) //each set, of polygons 4
                                    {

                                        for (int j = 0; j < geoArrayList[i][j].Length; j++) //each set of coordinates e.g. 17
                                        {
                                            Coordinate[] imageOutlineCoordinates = new Coordinate[geoArrayList[i][j].Length];

                                            for (int k = 0; k < geoArrayList[i][j].Length; k++)
                                            {
                                                double[] coordinates = new double[2];

                                                coordinates[0] = geoArrayList[i][j][k][0];
                                                coordinates[1] = geoArrayList[i][j][k][1];

                                                Coordinate coordinate = new Coordinate(coordinates[0], coordinates[1]);

                                                imageOutlineCoordinates[k] = coordinate;
                                            };

                                            Polygon polygon = geometryFactory.CreatePolygon(imageOutlineCoordinates);
                                            var reversedPolygon = MakePolygonCounterClockWise(polygon);
                                            polygons[i] = reversedPolygon;
                                            break;
                                        }
                                    };

                                    //and eventually save to a multipolygon
                                    MultiPolygon multipolygon = geometryFactory.CreateMultiPolygon(polygons);
                                    //var reversedMultiPolygon = MakeMultiPolygonCounterClockWise(multipolygon);

                                    var lgaEntity = new LGA
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = lga.properties.NAME_2,
                                        Boundary = multipolygon,
                                        StateId = Guid.Parse(stateNamesandIdsList.Where(x => x.stateName == lga.properties.NAME_1).FirstOrDefault().stateId),

                                    };

                                    context.LGAs.Add(lgaEntity);
                                }

                                // Geography type is Polygon
                                else if (lga.geometry.type == "Polygon")
                                {
                                    var geoArrayList = lga.geometry.coordinates[0].ToObject<object[][]>();

                                    Coordinate[] imageOutlineCoordinates = new Coordinate[geoArrayList.Length];
                                    // extract coordinates in each feature
                                    for (int i = 0; i < geoArrayList.Length; i++)
                                    {
                                        double[] coordinates = new double[2];

                                        //double[] arr = properList[i].Cast<double>().ToArray();

                                        coordinates[0] = geoArrayList[i][0];
                                        coordinates[1] = geoArrayList[i][1];

                                        Coordinate coordinate = new Coordinate(coordinates[0], coordinates[1]);

                                        imageOutlineCoordinates[i] = coordinate;
                                    };

                                    Polygon polygon = geometryFactory.CreatePolygon(imageOutlineCoordinates);

                                    var reversedPolygon = MakePolygonCounterClockWise(polygon);

                                    var lgaEntity = new LGA
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = lga.properties.NAME_2,
                                        Boundary = reversedPolygon,
                                        StateId = Guid.Parse(stateNamesandIdsList.Where(x => x.stateName == lga.properties.NAME_1).FirstOrDefault().stateId),
                                        Created = DateTime.UtcNow.AddHours(1)

                                    };

                                    context.LGAs.Add(lgaEntity);
                                }
                            }

                            context.SaveChanges();
                        }
                        #endregion seed LGAs

                        #region
                        ////Seed default towns
                        if (!(context.Towns.Any()))
                        {
                            IList<Town> newAlertLevels = new List<Town>() {
                            new Town() { Id = Guid.NewGuid(), Name = "Victoria Island", LGAId = Guid.Parse("9776D0E5-CE88-4686-8CD2-FEE0EFC18186"), CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                            new Town() { Id = Guid.NewGuid(), Name = "Ikoyi", LGAId = Guid.Parse("9776D0E5-CE88-4686-8CD2-FEE0EFC18186"), CreatedBy = "Antman", Created = DateTime.UtcNow.AddHours(1)},
                            new Town() { Id = Guid.NewGuid(), Name = "Ajah", LGAId = Guid.Parse("9776D0E5-CE88-4686-8CD2-FEE0EFC18186"), CreatedBy = "Antman", Created = DateTime.UtcNow.AddHours(1)},
                            };

                            context.Towns.AddRange(newAlertLevels);
                        }
                        #endregion seed LGAs

                        context.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + $" {ex.InnerException.Message}");
            }
        }

        public void SeedAppTrooperHelpers()
        {
            //Default Alert Levels
            try
            {

                using (var serviceScope = _scopeFactory.CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                    {

                        #region seed alert levels
                        //Seed default alert levels
                        if (!(context.AlertLevels.Any()))
                        {
                            IList<AlertLevel> newAlertLevels = new List<AlertLevel>() {
                                new AlertLevel() { Id = Guid.NewGuid(), Name = "Neutral", Description = "Neutral", alertLevel = AlertLevelEnum.Neutral, Created = DateTime.UtcNow.AddHours(1)},
                                new AlertLevel() { Id = Guid.NewGuid(), Name = "Low", Description = "Low", alertLevel = AlertLevelEnum.Low, Created = DateTime.UtcNow.AddHours(1)},
                                new AlertLevel() { Id = Guid.NewGuid(), Name = "High", Description = "High", alertLevel = AlertLevelEnum.High, Created = DateTime.UtcNow.AddHours(1)},
                                new AlertLevel() { Id = Guid.NewGuid(), Name = "Critical", Description = "Critical", alertLevel = AlertLevelEnum.Critical, Created = DateTime.UtcNow.AddHours(1)},
                                new AlertLevel() { Id = Guid.NewGuid(), Name = "Moderate", Description = "Moderate", alertLevel = AlertLevelEnum.Moderate, Created = DateTime.UtcNow.AddHours(1)},
                            };

                            context.AlertLevels.AddRange(newAlertLevels);
                            context.SaveChanges();

                        }

                        #endregion seed alert levels

                        #region seed broadcast levels
                        //Seed default broadcast levels
                        if (!(context.BroadcastLevels.Any()))
                        {
                            IList<BroadcastLevel> broadcastLevels = new List<BroadcastLevel>() {
                        new BroadcastLevel() { Id = Guid.NewGuid(), Name = "Settlement", Description = "Settlement", broadcastLevel = BroadcastLevelEnum.Settlement, Created = DateTime.UtcNow.AddHours(1)},
                        new BroadcastLevel() { Id = Guid.NewGuid(),  Name = "Town", Description = "Town",  broadcastLevel = BroadcastLevelEnum.Town, Created = DateTime.UtcNow.AddHours(1)},
                        new BroadcastLevel() { Id = Guid.NewGuid(),  Name = "LGA", Description = "LGA",  broadcastLevel = BroadcastLevelEnum.LGA, Created = DateTime.UtcNow.AddHours(1)},
                        new BroadcastLevel() { Id = Guid.NewGuid(),  Name = "State", Description = "State",  broadcastLevel = BroadcastLevelEnum.State, Created = DateTime.UtcNow.AddHours(1)},
                        new BroadcastLevel() { Id = Guid.NewGuid(),  Name = "Nationwide", Description = "Nationwide",  broadcastLevel = BroadcastLevelEnum.Nationwide, Created = DateTime.UtcNow.AddHours(1)}
                        };

                            context.BroadcastLevels.AddRange(broadcastLevels);
                            context.SaveChanges();

                        }
                        #endregion seed broadcast levels

                        #region seed broadcaster types
                        //Seed default broadcast levels
                        if (!(context.BroadcasterTypes.Any()))
                        {
                            IList<BroadcasterType> broadcasterTypes = new List<BroadcasterType>() {
                        new BroadcasterType() { Id = Guid.NewGuid(),Name = "Vigilant Admin", Description = "Vigilant Admin",  Broadcaster = BroadcasterTypeEnum.VigilantAdmin, Created = DateTime.UtcNow.AddHours(1)},
                        new BroadcasterType() { Id = Guid.NewGuid(),  Name = "Vigilant User", Description = "Vigilant User",  Broadcaster = BroadcasterTypeEnum.VigilantUser, Created = DateTime.UtcNow.AddHours(1)},
                        new BroadcasterType() { Id = Guid.NewGuid(), Name = "Security Authority", Description = "Security Authority",  Broadcaster = BroadcasterTypeEnum.SecurityAuthority, Created = DateTime.UtcNow.AddHours(1)},

                            };
                            context.BroadcasterTypes.AddRange(broadcasterTypes);
                            context.SaveChanges();

                        }

                        #endregion seed broadcaster types

                        #region seed source categories
                        //Seed default source cateories levels

                        if (!(context.SourceCategories.Any()))
                        {
                            IList<SourceCategory> newSourceCategories = new List<SourceCategory>() {
                            new SourceCategory() { Id = Guid.NewGuid(), CategoryName = "Online Blog", Description = "Online Blog", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                            new SourceCategory() { Id = Guid.NewGuid(), CategoryName = "Social Media", Description = "Social Media", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                            new SourceCategory() { Id = Guid.NewGuid(), CategoryName = "Newspaper", Description = "Newspaper", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                            new SourceCategory() { Id = Guid.NewGuid(), CategoryName = "Radio", Description = "Radio", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                            new SourceCategory() { Id = Guid.NewGuid(), CategoryName = "Magazine", Description = "Magazine", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                            new SourceCategory() { Id = Guid.NewGuid(), CategoryName = "Eye Witness", Description = "Eye Witness", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                            new SourceCategory() { Id = Guid.NewGuid(), CategoryName = "Word of Mouth", Description = "Word of Mouth", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                            new SourceCategory() { Id = Guid.NewGuid(), CategoryName = "Town Crier", Description = "Town Crier", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                            new SourceCategory() { Id = Guid.NewGuid(), CategoryName = "Security Authority", Description = "Such as Paramilitary or security agencies", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                            };

                            context.SourceCategories.AddRange(newSourceCategories);
                            context.SaveChanges();
                        }

                        #endregion seed default source categories 

                        #region seed sources
                        //Seed default source cateories levels
                        if (!(context.Sources.Any()))
                        {
                            IList<Source> sources = new List<Source>() {
                            new Source() { Id = Guid.NewGuid(), SourceName = "The Guardian", Description = "The Guardian", SourceCategoryId=Guid.Parse("96E31BDE-FAEC-4684-A4C8-592B594AC1B6"), CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                            new Source() { Id = Guid.NewGuid(), SourceName = "Instagram", Description = "Instagram", SourceCategoryId=Guid.Parse("56E968A2-D8B3-4244-ABC4-A08D810E6886"), CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                            new Source() { Id = Guid.NewGuid(), SourceName = "Cool FM", Description = "Cool FM", SourceCategoryId=Guid.Parse("7ABAC364-8F4F-4E23-94A9-0DF658A9D358"), CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                            new Source() { Id = Guid.NewGuid(), SourceName = "Vigilant User", Description = "Vigilant NG User", SourceCategoryId=Guid.Parse("831C8180-B1A2-46C9-AD9E-0F2AFE2D07FD"), CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)}

                            };

                            context.Sources.AddRange(sources);
                            context.SaveChanges();
                        }

                        #endregion seed sources

                        #region seed tip categories
                        //Seed default source cateories levels

                        if (!(context.SecurityTipCategoryTypes.Any()))
                        {
                            var categoryTypes = new List<SecurityTipCategoryType>
                        {
                            new SecurityTipCategoryType { Id = Guid.NewGuid(), Name = "Violence & Crime", Description = "Incidents involving violence or criminal acts against persons" },
                            new SecurityTipCategoryType { Id = Guid.NewGuid(), Name = "Property Crime", Description = "Criminal acts against property" },
                            new SecurityTipCategoryType { Id = Guid.NewGuid(), Name = "Public Safety", Description = "Incidents affecting public safety and welfare" },
                            new SecurityTipCategoryType { Id = Guid.NewGuid(), Name = "Public Order", Description = "Disruptions to public order and suspicious activities" },
                            new SecurityTipCategoryType { Id = Guid.NewGuid(), Name = "Environmental/Natural", Description = "Weather-related and environmental hazards" },
                            new SecurityTipCategoryType { Id = Guid.NewGuid(), Name = "Terrorism & Major Threats", Description = "Serious threats to public security" }
                        };

                            context.SecurityTipCategoryTypes.AddRange(categoryTypes);
                            context.SaveChanges();
                        }


                        // Seed data for the category types


                        //if (!(context.SecurityTipCategories.Any()))
                        //{
                        //    IList<SecurityTipCategory> newTipCategories = new List<SecurityTipCategory>() {
                        //    new SecurityTipCategory() { Id = Guid.NewGuid(), Name = "Robbery/Theft", Description = "Robbery/Theft", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                        //    new SecurityTipCategory() { Id = Guid.NewGuid(), Name = "Accident/Traffic", Description = "Accident/Traffic", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                        //    new SecurityTipCategory() { Id = Guid.NewGuid(), Name = "Arson/Fire", Description = "Arson/Fire", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                        //    new SecurityTipCategory() { Id = Guid.NewGuid(), Name = "Riot", Description = "Riot", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                        //    new SecurityTipCategory() { Id = Guid.NewGuid(), Name = "Terrorism", Description = "Terrorism", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                        //    new SecurityTipCategory() { Id = Guid.NewGuid(), Name = "Public Protests", Description = "Protest", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                        //    new SecurityTipCategory() { Id = Guid.NewGuid(), Name = "Confraternity/Cultism Activities", Description = "Confraternity/Cultism Activities", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                        //    new SecurityTipCategory() { Id = Guid.NewGuid(), Name = "Other", Description = "Other", CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)}

                        //    };

                        //    context.SecurityTipCategories.AddRange(newTipCategories);
                        //    context.SaveChanges();

                        //}

                        if (!(context.SecurityTipCategories.Any()))
                        {
                            // Get the category type IDs (assuming they're already in the database)
                            var violenceTypeId = context.SecurityTipCategoryTypes.FirstOrDefault(t => t.Name == "Violence & Crime")?.Id ??
                                                 new Guid("05D31B6C-2E4C-4186-85E7-DBEC6BD4B913");
                            var propertyTypeId = context.SecurityTipCategoryTypes.FirstOrDefault(t => t.Name == "Property Crime")?.Id ??
                                                 new Guid("C57D64DF-DB71-4CCD-BDF2-050F63513FC3");
                            var publicSafetyTypeId = context.SecurityTipCategoryTypes.FirstOrDefault(t => t.Name == "Public Safety")?.Id ??
                                                     new Guid("28F7F93B-4C40-492F-B168-08B3DAAC2559");
                            var publicOrderTypeId = context.SecurityTipCategoryTypes.FirstOrDefault(t => t.Name == "Public Order")?.Id ??
                                                    new Guid("AC3F6513-8B96-4111-87DC-88A6E14AF730");
                            var environmentalTypeId = context.SecurityTipCategoryTypes.FirstOrDefault(t => t.Name == "Environmental/Natural")?.Id ??
                                                      new Guid("A315533D-3532-4478-990E-EE4B76E16BBB");
                            var terrorismTypeId = context.SecurityTipCategoryTypes.FirstOrDefault(t => t.Name == "Terrorism & Major Threats")?.Id ??
                                                  new Guid("5B798595-D394-4B6C-8364-3D802295D75D");

                            // Create new categories with the structured categories we agreed on
                            IList<SecurityTipCategory> newTipCategories = new List<SecurityTipCategory>() {
                                // Violence & Crime
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Armed Robbery",
                                    Description = "Theft using weapons or threat of force",
                                    CategoryTypeId = violenceTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Assault/Attack",
                                    Description = "Physical attack against person(s)",
                                    CategoryTypeId = violenceTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Kidnapping",
                                    Description = "Abduction or hostage situation",
                                    CategoryTypeId = violenceTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
        
                                // Property Crime
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Vandalism",
                                    Description = "Damage to property, graffiti, destruction",
                                    CategoryTypeId = propertyTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Theft/Burglary",
                                    Description = "Theft of property, break-ins",
                                    CategoryTypeId = propertyTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
        
                                // Public Safety
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Accident/Traffic",
                                    Description = "Road incidents, accidents",
                                    CategoryTypeId = publicSafetyTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Fire/Hazard",
                                    Description = "Fires, gas leaks, dangerous materials",
                                    CategoryTypeId = publicSafetyTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Medical Emergency",
                                    Description = "Medical situations requiring assistance",
                                    CategoryTypeId = publicSafetyTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
        
                                // Public Order
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Civil Unrest/Protest",
                                    Description = "Demonstrations, riots, civil disturbances",
                                    CategoryTypeId = publicOrderTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Suspicious Activity",
                                    Description = "Unusual behavior, suspicious objects/persons",
                                    CategoryTypeId = publicOrderTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Confraternity/Cultism Activities",
                                    Description = "Gang or cult-related activities",
                                    CategoryTypeId = publicOrderTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
        
                                // Environmental/Natural
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Flooding",
                                    Description = "Floods, water hazards",
                                    CategoryTypeId = environmentalTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Severe Weather",
                                    Description = "Storms, extreme weather conditions",
                                    CategoryTypeId = environmentalTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
        
                                // Terrorism & Major Threats
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Terrorism",
                                    Description = "Terrorist acts or threats",
                                    CategoryTypeId = terrorismTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Mass Casualty Event",
                                    Description = "Events affecting many people",
                                    CategoryTypeId = terrorismTypeId,
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                },
        
                                // Miscellaneous - keep "Other" as a category under one of the types
                                new SecurityTipCategory() {
                                    Id = Guid.NewGuid(),
                                    Name = "Other",
                                    Description = "Other security incidents not listed",
                                    CategoryTypeId = publicSafetyTypeId, // You can choose which type to place "Other" under
                                    CreatedBy = "Antman",
                                    Created = DateTime.UtcNow.AddHours(1)
                                }
                            };

                            context.SecurityTipCategories.AddRange(newTipCategories);
                            context.SaveChanges();
                        }

                        #endregion seed tip categories 

                        #region subscription plans
                        //Seed default subscription plan
                        if (!(context.Subscriptions.Any()))
                        {
                            IList<Subscription> sources = new List<Subscription>() {
                            new Subscription() { Id = Guid.NewGuid(), SubscriptionName = "Basic", SubscriptionDescription = "Basic",  MonthlyFee=0.00M, CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},
                            new Subscription() { Id = Guid.NewGuid(), SubscriptionName = "Advanced", SubscriptionDescription = "Advanced", MonthlyFee=100.00M, CreatedBy="Antman", Created = DateTime.UtcNow.AddHours(1)},

                            };

                            context.Subscriptions.AddRange(sources);
                            context.SaveChanges();

                        }
                        #endregion subscription plans



                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + $": {ex.InnerException.Message}");
            }
        }
    }
}
