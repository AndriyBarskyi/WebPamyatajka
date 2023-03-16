using WebPamyatajka.Data;
using WebPamyatajka.Models;

namespace WebPamyatajka.Services.Impl;

public class CardService : ICardService
{
    private readonly ApplicationDbContext _context;

    public CardService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Card> GetAllByCategoryId(int categoryId)
    {
        return _context.Cards.ToList().FindAll(c => c.Category.Id.Equals(categoryId));
    }

    public Card GetById(int id)
    {
        Card card = _context.Cards.First(c => c.Id == id);
        CheckIfCardExists(card);
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
            throw new ArgumentException("Front of the card is required.", nameof(card));
        }

        if (string.IsNullOrEmpty(card.Back))
        {
            throw new ArgumentException("Back of the card is required.", nameof(card));
        }

        if (_context.Cards.Find() == card)
        {
            throw new ArgumentException("Such card already exists:", nameof(card));
        }

        _context.Cards.Add(card);
        _context.SaveChanges();
        return card;
    }

    public void Update(Card card)
    {
        if (card == null)
        {
            throw new ArgumentNullException(nameof(card));
        }

        if (string.IsNullOrEmpty(card.Front))
        {
            throw new ArgumentException("Front of the card is required.", nameof(card));
        }

        if (string.IsNullOrEmpty(card.Back))
        {
            throw new ArgumentException("Back of the card is required.", nameof(card));
        }

        _context.Cards.Update(card);
        _context.SaveChanges();
    }

    public void DeleteById(int cardId)
    {
        Card card = GetById(cardId);
        _context.Cards.Remove(card);
        _context.SaveChanges();
    }
}