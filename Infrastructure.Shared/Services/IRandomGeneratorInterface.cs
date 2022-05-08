using Application.DTOs;
using Application.Services.Interfaces;
using Infrastructure.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Shared.Services
{
    public interface IRandomNumberGeneratorInterface : IAutoDependencyService
    {
        string GenerateRandomNumber(int length, Mode mode = Mode.AlphaNumeric);
    }


}
