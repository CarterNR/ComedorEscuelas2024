using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
    public class BitacoraService : IBitacoraService
    {
        IUnidadDeTrabajo Unidad;
        SisComedorContext context;
        public BitacoraService(IUnidadDeTrabajo unidadDeTrabajo, SisComedorContext context)
        {
            this.Unidad = unidadDeTrabajo;
            this.context = context;
        }


        #region
        Bitacora Convertir(BitacoraDTO bitacora)
        {
            return new Bitacora
            {
                IdBitacora = bitacora.IdBitacora,
                Accion = bitacora.Accion,
                FechaHora = bitacora.FechaHora,
                Estado = bitacora.Estado,
                IdEscuela = bitacora.IdEscuela,
                IdUsuario = bitacora.IdUsuario
            };
        }

        BitacoraDTO Convertir(Bitacora bitacora)
        {
            return new BitacoraDTO
            {
                IdBitacora = bitacora.IdBitacora,
                Accion = bitacora.Accion,
                FechaHora = bitacora.FechaHora,
                Estado = bitacora.Estado,
                IdEscuela = bitacora.IdEscuela,
                IdUsuario = bitacora.IdUsuario
            };
        }
        #endregion






        public bool Agregar(BitacoraDTO bitacora)
        {
            Bitacora entity = Convertir(bitacora);
            Unidad.BitacoraDAL.Add(entity);
            return Unidad.Complete();
        }

        public bool Editar(BitacoraDTO bitacora)
        {
            var entity = Convertir(bitacora);
            Unidad.BitacoraDAL.Update(entity);
            return Unidad.Complete();
        }

        public bool Eliminar(BitacoraDTO bitacora)
        {
            var entity = Convertir(bitacora);
            Unidad.BitacoraDAL.Remove(entity);
            return Unidad.Complete();
        }

        public BitacoraDTO Obtener(int id)
        {
            return Convertir(Unidad.BitacoraDAL.Get(id));
        }

        public List<BitacoraDTO> Obtener()
        {
            List<BitacoraDTO> list = new List<BitacoraDTO>();
            var bitacoras = Unidad.BitacoraDAL.GetAll()
                .Join(
                    context.Usuarios,
                    bitacora => bitacora.IdUsuario,
                    usuario => usuario.IdUsuario,
                    (bitacora, usuario) => new
                    {
                        Bitacora = bitacora,
                        Usuario = usuario
                    })
                .Join(
                    context.Escuelas,
                    joined => joined.Bitacora.IdEscuela,
                    escuela => escuela.IdEscuela,
                    (joined, escuela) => new
                    {
                        IdBitacora = joined.Bitacora.IdBitacora,
                        Accion = joined.Bitacora.Accion,
                        FechaHora = joined.Bitacora.FechaHora,
                        Estado = joined.Bitacora.Estado,
                        IdEscuela = joined.Bitacora.IdEscuela,
                        NombreEscuela = escuela.NombreEscuela,
                        IdUsuario = joined.Bitacora.IdUsuario,
                        NombreCompleto = joined.Usuario.NombreCompleto
                    })
                .ToList();

            foreach (var item in bitacoras)
            {
                var bitacoraDTO = new BitacoraDTO
                {
                    IdBitacora = item.IdBitacora,
                    Accion = item.Accion,
                    FechaHora = item.FechaHora,
                    Estado = item.Estado,
                    IdEscuela = item.IdEscuela,
                    NombreEscuela = item.NombreEscuela,
                    IdUsuario = item.IdUsuario,
                    NombreCompleto = item.NombreCompleto
                };
                list.Add(bitacoraDTO);
            }
            return list;
        }
    }
}
