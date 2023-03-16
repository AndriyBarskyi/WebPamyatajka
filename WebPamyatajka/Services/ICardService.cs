using WebPamyatajka.Models;

namespace WebPamyatajka.Services;

public interface ICardService
{
    List<Card> GetAllByCategoryId(int categoryId);
    Card GetById(int id);
    Card Create(Card card);
    void Update(Card card);
    void DeleteById(int cardId);
}