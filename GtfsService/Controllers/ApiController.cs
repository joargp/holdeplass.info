using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GtfsService.Models;

public class WebApiController : ApiController
{
    private readonly IStopRepository _notatweetRepository;
    public WebApiController(IStopRepository notatweetRepository)
    {
        this._notatweetRepository = notatweetRepository;
    }
    // If you are using Dependency Injection, you can delete the following constructor
        public WebApiController() : this(new StopRepository())
        {
        }

    // GET /api/notatwitterapi
    public IQueryable<Stop> Get()
    {
        return _notatweetRepository.All;
    }

    // GET /api/notatwitterapi/5
    public Stop Get(int id)
    {
        var notatweet = _notatweetRepository.Find(id);
        if (notatweet == null)
            throw new HttpResponseException(HttpStatusCode.NotFound);
        return notatweet;
    }

    // POST /api/notatwitterapi
    public HttpResponseMessage Post(Stop value)
    {
        if (ModelState.IsValid)
        {
            _notatweetRepository.InsertOrUpdate(value);
            _notatweetRepository.Save();

            //Created!
            var response = new HttpResponseMessage(HttpStatusCode.Created);

            //Let them know where the new NotATweet is
            string uri = Url.Route(null, new { id = value.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);

            return response;

        }
        throw new HttpResponseException(HttpStatusCode.BadRequest);
    }

    // PUT /api/notatwitterapi/5
    public HttpResponseMessage Put(int id, Stop value)
    {
        if (ModelState.IsValid)
        {
            _notatweetRepository.InsertOrUpdate(value);
            _notatweetRepository.Save();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
        throw new HttpResponseException(HttpStatusCode.BadRequest);
    }

    // DELETE /api/notatwitterapi/5
    public void Delete(int id)
    {
        var notatweet = _notatweetRepository.Find(id);
        if (notatweet == null)
            throw new HttpResponseException(HttpStatusCode.NotFound);

        _notatweetRepository.Delete(id);
    }
}
