﻿using Microsoft.Extensions.Options;
using RealEstateApi.HttpClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApi.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IZillowApi _zillowApi;
        private readonly ZillowApiSettings _zillowApiSettings;

        public PropertyService(IZillowApi zillowApi, IOptions<ZillowApiSettings> zillowApiSettings)
        {
            _zillowApi = zillowApi;
            _zillowApiSettings = zillowApiSettings.Value;
        }

        public async Task<IEnumerable<Property>> GetSoldProperties(GetSoldPropertiesRequest request)
        {
            var response = await _zillowApi.GetSearchResults(_zillowApiSettings.ApiKey, $"{request.City}, {request.Country}", request.Address);

            if (response.Message.Code > 0)
            {
                throw new Exception($"Zillow API returned error code: {response.Message.Code} - {response.Message.Text}");
            }

            var properties = response.Response.Results.Properties.Where(p => p.LastSoldDate != null).Take(_zillowApiSettings.PageSize);

            return properties;
        }
    }
}