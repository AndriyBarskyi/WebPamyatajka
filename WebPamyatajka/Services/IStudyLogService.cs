using WebPamyatajka.Models;

namespace WebPamyatajka.Services;

public interface IStudyLogService
{
    StudyLog? GetByCardAndUserId(int cardId, string userId);
    StudyLog? GetCardToReviewByCardAndUserId(int cardId, string userId);
    bool Exists(int cardId, string userId);
    StudyLog Create(int cardId);
    StudyLog Remember(int cardId);
    StudyLog NotRemember(int cardId);
    StudyLog MarkKnown(int cardId);
    void Delete(int cardId);
}