namespace Library.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }


        #region Create

        [HttpPost]

        public async Task<ActionResult<BooksResponse>> AddBook([FromBody] CreateBookRequest book)
        {

            var books = await _bookService.AddBookAsync(book);

            var response = new BooksResponse
            {             
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                ISBN = book.ISBN,
                AvailableCopies = book.TotalCopies,
                TotalCopies = book.TotalCopies,
                IsActive = book.IsActive,
                CreatedAt = DateTime.UtcNow,
            };

            return Ok(response);


        }

        #endregion

        #region Read

        [HttpGet]

        public async Task<IActionResult> GetBooks(CancellationToken cancellationToken)
        {

            var Books = await _bookService.GetAllBooksAsync(cancellationToken);

            if (Books == null || !Books.Any())
            {
                return NotFound("No members found.");
            }

            var response = Books.Select(b => new BooksResponse
            {
                Title = b.Title,
                Author = b.Author,
                Genre = b.Genre,
                ISBN = b.ISBN,
                AvailableCopies = b.TotalCopies,
                TotalCopies = b.TotalCopies,
                IsActive = b.IsActive,
                CreatedAt = DateTime.UtcNow,
                Borrowings = b.Borrowings,

            });
               
                
                
            return Ok(response);
        }


        [HttpGet("Search")]

        public async Task<IActionResult> FilterBooks([FromQuery] string? author, [FromQuery] string? Genre)
        {

            var results = await _bookService.GetBooksAsync(author, Genre);

            return Ok(results);
        }

        #endregion

        #region Update

        [HttpPut("{id:int}")]

        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBooksRequest updateBooksRequest)
        {
            if (id != updateBooksRequest.BookId)
            {
                return BadRequest();
            }
            await _bookService.UpdateBookAsync(updateBooksRequest);
            return NoContent();
        }

        #endregion


    }
}
