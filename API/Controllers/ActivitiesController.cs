
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        private readonly IMediator mediator;

        public ActivitiesController(IMediator mediator)
        {
            this.mediator = mediator;


        }


        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities(CancellationToken ct )
        {
            return await Mediator.Send(new List.Query(),ct);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return await Mediator.Send(new Details.Query{Id = id});
        }

        [HttpPost]

        //IActionResult->  And when we use our action results, it gives us it gives us access to the to be response types such as return, OK, return, bad request, return not found.But we don't need to specify the type of thing that we're returning here and we'll just call this method



        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            return Ok(await Mediator.Send(new Create.Command{Activity = activity}));
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
            activity.Id = id;
            return Ok(await Mediator.Send(new Edit.Command{Activity = activity}));
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteActivity(Guid id ){

            return Ok(await Mediator.Send(new Delete.Command{Id = id}));
        }
    }
}