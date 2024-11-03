using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Client;
using Confiti.MoySklad.Remap.Entities;

namespace ProfitAPI.Services;
public class MoySkladService
{
    private readonly MoySkladApi _api;

    public MoySkladService(string accessToken)
    {
        var credentials = new MoySkladCredentials { AccessToken = accessToken };
        _api = new MoySkladApi(credentials);
    }

    // Пример метода для получения всех товаров
    public async Task<Assortment[]> GetProductsAsync()
    {
        var response = await _api.Assortment.GetAllAsync();
        return response.Payload.Rows; 
    }
}