using WebPamyatajka.Models;

namespace WebPamyatajka.Services;

public interface ICardService
{
    List<Card> GetAll(int categoryId);
    public int CountCards(int categoryId);
    Card GetById(int id);
    public Card? GetNextCardForReview(int categoryId);
    public Card? GetNextCardToLearn(int categoryId);
    Card Create(Card card);
    Card Update(Card newCard);
    void DeleteById(int cardId);
    public Card Move(Card newCard);
}