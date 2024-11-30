using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.server.models;
using CRUD.Compartir;
using Microsoft.EntityFrameworkCore;

namespace webapi.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class HistorialController : ControllerBase
    {

        private readonly PruebaContext _dbContext;

        public HistorialController(PruebaContext dbContext)
        {
            _dbContext = dbContext;

        }


        [HttpGet]
        [Route("lista/{idus}")]
        public async Task<IActionResult> lista(int idus)
        {
            var resp = new Responses<List<HistorialClass>>();
            var listahistorial = new List<HistorialClass>();

            try
            {
                var tod = await _dbContext.Historials.ToListAsync();
                var xx =tod.Where(c=> c.IdUser== idus).ToList();
                //foreach (var item in await _dbContext.Historials.ToListAsync())
                // foreach (var item in await _dbContext.Historials.Where(x => x.IdUser == idus).ToListAsync())
                foreach (var item in xx)
                {
                    listahistorial.Add(new HistorialClass
                    {
                        Id = item.Id,
                        IdUser = item.IdUser,
                        Number = item.Number,
                        NumResp=item.NumResp,
                        Fecha = item.Fecha,
                        
                    });
                }

                resp.Correcto = true;
                resp.Valores = listahistorial;
            }
            catch (Exception exx)
            {
                resp.Correcto = false;
                resp.Mensaje = exx.Message;
            }

            return Ok(resp);
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<IActionResult> buscar(int id)
        {
            var resp = new Responses<HistorialClass>();
            var datoshistorial = new HistorialClass();


            try
            {
                var dbUsers = await _dbContext.Historials.FirstOrDefaultAsync(x => x.Number == id);

                if (dbUsers != null)
                {
                    datoshistorial.Id = dbUsers.Id;
                    datoshistorial.IdUser = dbUsers.IdUser;
                    datoshistorial.Number = dbUsers.Number;
                    datoshistorial.NumResp = dbUsers.NumResp;
                    datoshistorial.Fecha = dbUsers.Fecha;

                    resp.Correcto = true;
                    resp.Valores = datoshistorial;
                }
                else
                {
                    resp.Correcto = false;
                    resp.Mensaje = "Datos no encontrados";
                }



            }
            catch (Exception exx)
            {
                resp.Correcto = false;
                resp.Mensaje = exx.Message;
            }

            return Ok(resp);
        }


        [HttpPost]
        [Route("guardar")]
        public async Task<IActionResult> guardar(HistorialClass newhist)
        {
            var resp = new Responses<int>();

            try
            {
                var dbUsers = new Historial
                {
                    Id = newhist.Id,
                    IdUser = newhist.IdUser,
                    Number = newhist.Number,
                    NumResp = newhist.NumResp,
                    Fecha = newhist.Fecha,

                };

                _dbContext.Historials.Add(dbUsers);
                await _dbContext.SaveChangesAsync();

                if (dbUsers.Id != 0)
                {
                    resp.Correcto = true;
                    resp.Valores = dbUsers.Id;
                }
                else
                {
                    resp.Correcto = false;
                    resp.Mensaje = "No guardados";
                }


            }
            catch (Exception exx)
            {
                resp.Correcto = false;
                resp.Mensaje = exx.Message;
            }

            return Ok(resp);
        }


        [HttpPut]
        [Route("editar/{id}")]
        public async Task<IActionResult> editar(HistorialClass newuser, int id)
        {
            var resp = new Responses<int>();

            try
            {
                var dbUsers = await _dbContext.Historials.FirstOrDefaultAsync(e => e.Id == id);



                if (dbUsers != null)
                {
                    dbUsers.Fecha = newuser.Fecha;
                  

                    _dbContext.Historials.Update(dbUsers);
                    await _dbContext.SaveChangesAsync();

                    resp.Correcto = true;
                    resp.Valores = dbUsers.Id;
                }
                else
                {
                    resp.Correcto = false;
                    resp.Mensaje = "historial no encontrado";
                }


            }
            catch (Exception exx)
            {
                resp.Correcto = false;
                resp.Mensaje = exx.Message;
            }

            return Ok(resp);
        }


        [HttpDelete]
        [Route("borrar/{id}")]
        public async Task<IActionResult> borrar(int id)
        {
            var resp = new Responses<int>();

            try
            {
                var dbUsers = await _dbContext.Historials.FirstOrDefaultAsync(e => e.Id == id);



                if (dbUsers != null)
                {

                    _dbContext.Historials.Remove(dbUsers);
                    await _dbContext.SaveChangesAsync();

                    resp.Correcto = true;
                }
                else
                {
                    resp.Correcto = false;
                    resp.Mensaje = "Usuario no encontrado";
                }


            }
            catch (Exception exx)
            {
                resp.Correcto = false;
                resp.Mensaje = exx.Message;
            }

            return Ok(resp);
        }

    }
}
