using System;
using System.Linq;
using System.Web.Http;
using Pacman.DataAccess.EF;

namespace Pacman.Server.Controllers
{
    [RoutePrefix("api/maps")]
    public class MapsController : ApiController
    {
        
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetMap(int id)
        {
            try
            {
                using (var context = new PacmanDbContext())
                {
                    var map = context.Maps
                        .Include("Cells")
                        .FirstOrDefault(m => m.Id == id);

                    if (map == null)
                        return NotFound();

                    return Ok(map);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        }
        [HttpGet]
        [Route("name/{name}")]
        public IHttpActionResult GetMapByName(string name)
        {
            try
            {
                using (var context = new PacmanDbContext())
                {
                    var map = context.Maps
                        .Include("Cells")
                        .FirstOrDefault(m => m.Name == name);

                    if (map == null)
                        return NotFound();

                    return Ok(map);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
