﻿using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Helpers.Implementations
{
    public class ProveedorHelper : IProveedorHelper
    {
        IServiceRepository _ServiceRepository;

        Proveedor Convertir(ProveedorViewModel proveedor)
        {
            return new Proveedor
            {
                IdProveedor = proveedor.IdProveedor,
                NombreProveedor = proveedor.NombreProveedor,
                Telefono = proveedor.Telefono,
                CorreoElectronico = proveedor.CorreoElectronico,
                Direccion = proveedor.Direccion,
                Estado = proveedor.Estado,
                IdEscuela = proveedor.IdEscuela

    };
        }


        public ProveedorHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }

        public ProveedorViewModel Add(ProveedorViewModel proveedor)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Proveedor", Convertir(proveedor));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return proveedor;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Proveedor/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode) { var content = responseMessage.Content; }



        }

        public List<ProveedorViewModel> GetProveedores()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Proveedor");
            List<Proveedor> proveedores = new List<Proveedor>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                proveedores = JsonConvert.DeserializeObject<List<Proveedor>>(content);
            }

            List<ProveedorViewModel> resultado = new List<ProveedorViewModel>();
            foreach (var item in proveedores)
            {
                resultado.Add(
                            new ProveedorViewModel
                            {
                                IdProveedor = item.IdProveedor,
                                NombreProveedor = item.NombreProveedor,
                                Telefono = item.Telefono,
                                CorreoElectronico = item.CorreoElectronico,
                                Direccion = item.Direccion,
                                Estado = item.Estado,
                                IdEscuela = item.IdEscuela
                            }
                    );
            }
            return resultado;

        }

        public ProveedorViewModel GetProveedor(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Proveedor/" + id.ToString());
            Proveedor proveedor = new Proveedor();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                proveedor = JsonConvert.DeserializeObject<Proveedor>(content);
            }

            ProveedorViewModel resultado =
                            new ProveedorViewModel
                            {
                                IdProveedor = proveedor.IdProveedor,
                                NombreProveedor = proveedor.NombreProveedor,
                                Telefono = proveedor.Telefono,
                                CorreoElectronico = proveedor.CorreoElectronico,
                                Direccion = proveedor.Direccion,
                                Estado = proveedor.Estado,
                                IdEscuela = proveedor.IdEscuela
                            };


            return resultado;
        }

        public ProveedorViewModel Update(ProveedorViewModel proveedor)
        {
            HttpResponseMessage response = _ServiceRepository.PutResponse("api/Proveedor", Convertir(proveedor));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return proveedor;
        }
    }
}