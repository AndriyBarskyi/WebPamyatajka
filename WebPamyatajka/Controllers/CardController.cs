using Microsoft.AspNetCore.Mvc;
using WebPamyatajka.Models;
using WebPamyatajka.Services;

namespace WebPamyatajka.Controllers;

public class CardController : Controller
{
    private readonly ICardService _cardService;
    private readonly ICategoryService _categoryService;
    

    public CardController(ICardService cardService, ICategoryService
        categoryService)
    {
        _cardService = cardService;
        _categoryService = categoryService;
    }


    // GET: Card/Cards/5
    public IActionResult Cards(int categoryId)
    {
        var category = _categoryService.GetById(categoryId);
        ViewData["CategoryName"] = category.Name;
        return View(_cardService.GetAll(categoryId));
    }

    // GET: Card/Create
    [HttpPost]
    public ActionResult<Card> Create([FromBody] Card card)
    {
        var addedCard = _cardService.Create(card);
        return Ok(addedCard);
    }

    public int Count(int categoryId)
    {
        return _cardService.CountCards(categoryId);
    }
    public IActionResult GetNextCard(int categoryId, bool isLearn)
    {
        Card? card;
        card = isLearn
            ? _cardService.GetNextCardToLearn(categoryId)
            : _cardService.GetNextCardForReview(categoryId);

        return card != null ? View(card) : View();
    }

    [HttpGet]
    public List<Card> GetCardsByCategoryId(int categoryId)
    {
        return _cardService.GetAll(categoryId);
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _cardService.DeleteById(id);
        return Ok();
    }

    [HttpPatch]
    public IActionResult Update([FromBody] Card card)
    {
        var updatedCard = _cardService.Update(card);
        return Ok(updatedCard);
    }

    [HttpPatch]
    public IActionResult Move([FromBody] Card card)
    {
        var movedCard = _cardService.Move(card);
        return Ok(movedCard);
    }
}