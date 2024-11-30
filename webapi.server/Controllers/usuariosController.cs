using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.server.models;
using CRUD.Compartir;
using Microsoft.EntityFrameworkCore;

namespace webapi.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly PruebaContext _dbContext;

        public usuariosController(PruebaContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> lista()
        {
            //var respuesta = _dbContext.Users.ToList();
            var resp = new Responses<List<Clase1>>();
            var listaClase1 = new List<Clase1>();

            try {
                foreach(var item in await _dbContext.User.ToListAsync())
                {
                    listaClase1.Add(new Clase1
                    {
                        Id = item.Id,
                        Username=item.Username,
                        Pass=item.Pass,

                    });
                }

                resp.Correcto = true;
                resp.Valores = listaClase1;
            }
            catch (Exception exx) {
                resp.Correcto = false;
                resp.Mensaje = exx.Message;
            }

            return Ok(resp);
        }

        [HttpGet]
        [Route("buscar/{username}")]
        public async Task<IActionResult> buscar(string username)
        {
            var resp = new Responses<Clase1>();
            var datosClase1 = new Clase1();


            try
            {
                var dbUsers =  await _dbContext.User.FirstOrDefaultAsync(x => x.Username == username);

                if (dbUsers!=null)
                {
                    datosClase1.Id = dbUsers.Id;
                    datosClase1.Username = dbUsers.Username;
                    datosClase1.Pass = dbUsers.Pass;
                    
                    resp.Correcto = true;
                    resp.Valores = datosClase1;
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
        public async Task<IActionResult> guardar(Clase1 newuser)
        {
            var resp = new Responses<int>();

            try
            {
                var dbUsers = new User {
                    Id = newuser.Id,
                    Username = newuser.Username,
                    Pass = newuser.Pass,

                };

                _dbContext.User.Add(dbUsers);
                await _dbContext.SaveChangesAsync();

                if(dbUsers.Id !=0)
                {
                    resp.Correcto = true;
                    resp.Valores = dbUsers.Id;
                }
                else { 
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
        public async Task<IActionResult> editar(Clase1 newuser, int id)
        {
            var resp = new Responses<int>();

            try
            {
                var dbUsers = await _dbContext.User.FirstOrDefaultAsync(e => e.Id == id); 

               
               
                if (dbUsers != null)
                {
                    dbUsers.Username = newuser.Username;
                    dbUsers.Pass = newuser.Pass;


                    _dbContext.User.Update(dbUsers);
                    await _dbContext.SaveChangesAsync();

                    resp.Correcto = true;
                    resp.Valores = dbUsers.Id;
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



        [HttpDelete]
        [Route("borrar/{id}")]
        public async Task<IActionResult> borrar(int id)
        {
            var resp = new Responses<int>();

            try
            {
                var dbUsers = await _dbContext.User.FirstOrDefaultAsync(e => e.Id == id);



                if (dbUsers != null)
                {

                    _dbContext.User.Remove(dbUsers);
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
