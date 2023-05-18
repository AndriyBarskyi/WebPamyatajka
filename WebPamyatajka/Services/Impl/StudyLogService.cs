using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebPamyatajka.Data;
using WebPamyatajka.Models;

namespace WebPamyatajka.Services.Impl;

public class StudyLogService : IStudyLogService
{
    private const string
        NotFoundStudyLogWithCardIdAndUserId = "Not found study log with cardId and userId: ";

    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const int InitialIntervalInHours = 8;
    private const double IntervalMultiplier = 2.4;
    private const int MaxInterval = 90;

    public StudyLogService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public StudyLog? GetByCardAndUserId(int cardId, string userId)
    {
        StudyLog? studyLog = _context.StudyLogs.FirstOrDefault(sL => sL.CardId ==
            cardId && sL.UserId == userId);
        return studyLog;
    }

    public StudyLog? GetCardToReviewByCardAndUserId(int cardId, string userId)
    {
        StudyLog? studyLog = _context.StudyLogs.FirstOrDefault(sL => sL.CardId ==
            cardId && sL.UserId == userId && sL.NextViewAt.CompareTo(DateTime.Now) < 0);
        return studyLog;
    }

    public bool Exists(int cardId, string userId)
    {
        return _context.StudyLogs.FirstOrDefault(sL => sL.CardId ==
            cardId && sL.UserId == userId) != null;
    }
    
    public StudyLog Create(int cardId)
    {
        string userId = _httpContextAccessor.HttpContext.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;
        StudyLog newStudyLog = new StudyLog();
        newStudyLog.CardId = cardId;
        DateTime dateTime = DateTime.Now;
        newStudyLog.LastViewedAt = dateTime;
        newStudyLog.NextViewAt = dateTime.AddHours(InitialIntervalInHours);
        newStudyLog.UserId = userId;
        _context.StudyLogs.Add(newStudyLog);
        _context.SaveChanges();
        return newStudyLog;
    }

    public StudyLog Remember(int cardId)
    {
        string userId = _httpContextAccessor.HttpContext.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;
        StudyLog? studyLog = _context.StudyLogs.FirstOrDefault(sL => sL.CardId ==
            cardId && sL.UserId.Equals(userId));
        if (studyLog == null)
        {
            throw new ArgumentNullException(NotFoundStudyLogWithCardIdAndUserId +
                                            cardId + " and "+ userId);
        }

        int reviewDaysInterval = studyLog.NextViewAt.Subtract
            (studyLog.LastViewedAt).Days > 1
            ? (int)Math.Round((studyLog.NextViewAt.Subtract
                (studyLog.LastViewedAt).Days + IntervalMultiplier) * 2)
            : 1;
        DateTime dateTime = DateTime.Now;
        studyLog.NextViewAt = dateTime.AddDays(reviewDaysInterval);
        studyLog.LastViewedAt = dateTime;
        if (studyLog.NextViewAt.Subtract(studyLog.LastViewedAt).CompareTo(new
                TimeSpan(MaxInterval, 0, 0, 0)) >= 0)
        {
            studyLog.IsLearnt = true;
        }

        _context.StudyLogs.Update(studyLog);
        _context.SaveChanges();
        return studyLog;
    }

    public StudyLog NotRemember(int cardId)
    {
        string userId = _httpContextAccessor.HttpContext.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;
        StudyLog? studyLog = _context.StudyLogs.FirstOrDefault(sL => sL.CardId ==
            cardId && sL.UserId.Equals(userId));
        if (studyLog == null)
        {
            throw new ArgumentNullException(NotFoundStudyLogWithCardIdAndUserId +
                                            cardId + " and "+ userId);
        }

        int reviewDaysInterval = studyLog.NextViewAt.Subtract
            (studyLog.LastViewedAt).Days;
        DateTime dateTime = DateTime.Now;
        if (reviewDaysInterval > 1)
        {
            studyLog.LastViewedAt = dateTime.Subtract(TimeSpan.FromDays(
                (int)Math.Round(
                    studyLog.NextViewAt.Subtract(studyLog.LastViewedAt).Days /
                    2.0 -
                    IntervalMultiplier)));
            studyLog.NextViewAt = dateTime;
        }
        else
        {
            studyLog.NextViewAt = dateTime;
            studyLog.LastViewedAt = dateTime.Subtract(TimeSpan.FromHours(8));
        }

        _context.StudyLogs.Update(studyLog);
        _context.SaveChanges();
        return studyLog;
    }

    public StudyLog MarkKnown(int cardId)
    {
        string? userId = _httpContextAccessor.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;
        StudyLog newStudyLog = new StudyLog();
        newStudyLog.IsKnown = true;
        newStudyLog.UserId = userId;
        newStudyLog.CardId = cardId;
        _context.StudyLogs.Add(newStudyLog);
        _context.SaveChanges();
        return newStudyLog;
    }

    public void Delete(int cardId)
    {
        string? userId = _httpContextAccessor.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;
        StudyLog? studyLog = GetByCardAndUserId(cardId, userId);
        if (studyLog == null)
        {
            return;
        }
        _context.StudyLogs.Remove(studyLog);
        _context.SaveChanges();
    }
}