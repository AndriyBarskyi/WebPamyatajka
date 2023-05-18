using Microsoft.AspNetCore.Mvc;
using WebPamyatajka.Services;

namespace WebPamyatajka.Controllers
{
    public class StudyLogController : Controller
    {
        private readonly IStudyLogService _studyLogService;

        public StudyLogController(IStudyLogService studyLogService)
        {
            _studyLogService = studyLogService;
        }

        [HttpPost]
        public ActionResult Learn(int cardId)
        {
            return Ok(_studyLogService.Create(cardId));
        }
        
        [HttpPut]
        public ActionResult Remember(int cardId)
        {
            return Ok(_studyLogService.Remember(cardId));
        }
        
        [HttpPut]
        public ActionResult NotRemember(int cardId)
        {
            return Ok(_studyLogService.NotRemember(cardId));
        }
        
        [HttpPut]
        public ActionResult MarkKnown(int cardId)
        {
            return Ok(_studyLogService.MarkKnown(cardId));
        }
        
        [HttpDelete]
        public ActionResult Delete(int cardId)
        {
            _studyLogService.Delete(cardId);
            return Ok();
        }
    }
}