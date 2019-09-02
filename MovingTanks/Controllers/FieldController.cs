using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MovingTanks.Models.Classes;

namespace MovingTanks.Controllers
{
    [EnableCors("MyPolicy")]
    public class FieldController : ControllerBase
    {
        [HttpGet]
        [Route("~/api/field/setspeed/")]
        public JsonResult SetSpeed(sbyte speed)
        {
            bool result;
            if (speed < 0 || speed > 20)
                result = false;
            else
            {
                State.Speed = speed;
                result = true;
            }
            return new JsonResult(new { result, speed = State.Speed });
        }

        [HttpGet]
        [Route("~/api/field/getfield/")]
        public JsonResult GetField()
        {
            return new JsonResult(new {result = State.IsOk, field = State.Field, tanks = State.Objects.Where(x => x is Tank), obstacles = State.Objects.Where(x => x is Obstacle), speed = State.Speed });
        }
    }
}
