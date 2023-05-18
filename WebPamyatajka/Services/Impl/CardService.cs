using System.Security.Claims;
using WebPamyatajka.Data;
using WebPamyatajka.Models;

namespace WebPamyatajka.Services.Impl;

public class CardService : ICardService
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStudyLogService _studyLogService;

    public CardService(ApplicationDbContext context,
        IStudyLogService studyLogService,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _studyLogService = studyLogService;
        _httpContextAccessor = httpContextAccessor;
    }

    public List<Card> GetAll(int categoryId)
    {
        return _context.Cards.ToList()
            .FindAll(c => c.CategoryId.Equals(categoryId));
    }


    public int CountCards(int categoryId)
    {
        return (_context.Cards.ToList().FindAll(c => c.CategoryId.Equals
            (categoryId))).Count;
    }

    public Card GetById(int id)
    {
        Card card = _context.Cards.First(c => c.Id == id);
        CheckIfCardExists(card);
        return card;
    }

    public Card? GetNextCardForReview(int categoryId)
    {
        string userId = _httpContextAccessor.HttpContext.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;
        List<Card> cards = GetAll(categoryId).FindAll(c => _studyLogService
            .Exists(c.Id, userId) && !_studyLogService
            .GetByCardAndUserId(c.Id, userId)!.IsKnown && !_studyLogService
            .GetByCardAndUserId(c.Id, userId)!.IsLearnt);
        if (!cards.Exists(c => _studyLogService
        .GetCardToReviewByCardAndUserId(c.Id, userId) != null))
        {
            return null;
        }

        Card? card = cards.MinBy(c =>
            _studyLogService.GetCardToReviewByCardAndUserId(c.Id, userId)?
                .NextViewAt);
        return card;
    }

    public Card? GetNextCardToLearn(int categoryId)
    {
        string userId = _httpContextAccessor.HttpContext.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Card? card = GetAll(categoryId).Find(c => !_studyLogService
            .Exists(c.Id, userId));
        return card;
    }

    private static void CheckIfCardExists(Card card)
    {
        if (card == null)
        {
            throw new ArgumentException(nameof(card));
        }
    }

    public Card Create(Card card)
    {
        if (card == null)
        {
            throw new ArgumentNullException(nameof(card));
        }

        if (string.IsNullOrEmpty(card.Front))
        {
            throw new ArgumentException("Front of the card is required.",
                nameof(card));
        }

        if (string.IsNullOrEmpty(card.Back))
        {
            throw new ArgumentException("Back of the card is required.",
                nameof(card));
        }

        if (_context.Cards.Any(c => c.Id == card.Id))
        {
            throw new ArgumentException("Such card already exists:",
                nameof(card));
        }

        _context.Cards.Add(card);
        _context.SaveChanges();
        return card;
    }

    public Card Update(Card newCard)
    {
        if (newCard == null)
        {
            throw new ArgumentNullException(nameof(newCard));
        }

        if (string.IsNullOrEmpty(newCard.Front))
        {
            throw new ArgumentException("Front of the card is required.",
                nameof(newCard));
        }

        if (string.IsNullOrEmpty(newCard.Back))
        {
            throw new ArgumentException("Back of the card is required.",
                nameof(newCard));
        }

        Card card = _context.Cards.FirstOrDefault(c => c.Id == newCard.Id);
        CheckIfCardExists(card);
        card.Front = newCard.Front;
        card.Back = newCard.Back;
        _context.Cards.Update(card);
        _context.SaveChanges();
        return newCard;
    }

    public Card Move(Card newCard)
    {
        if (newCard == null)
        {
            throw new ArgumentNullException(nameof(newCard));
        }

        Card card = _context.Cards.FirstOrDefault(c => c.Id == newCard.Id);
        CheckIfCardExists(card);
        card.CategoryId = newCard.CategoryId;
        _context.Cards.Update(card);
        _context.SaveChanges();
        return newCard;
    }

    public void DeleteById(int cardId)
    {
        Card card = GetById(cardId);
        _context.Cards.Remove(card);
        _context.SaveChanges();
    }
}