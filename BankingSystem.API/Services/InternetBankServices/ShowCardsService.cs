using Repositories.DTOs;
using Repositories.InternetBankingRepositories;

namespace Services.InternetBankingServices
{
    public interface IShowCardsService
    {
        Task<ICollection<CardsDto>> ShowCardsList(string userId);
    }

    public class ShowCardsService : IShowCardsService
    {
        private readonly IShowCardsRepository _showCardsRepository;

        public ShowCardsService(IShowCardsRepository showCardsListRepository)
        {
            _showCardsRepository = showCardsListRepository;
        }

        public async Task<ICollection<CardsDto>> ShowCardsList(string userId)
        {
            try
            {
                var cardsList = await _showCardsRepository.ShowCards(userId);

                var cardsResult = new List<CardsDto>();

                for (int i = 0; i < cardsList.Count; i++)
                {
                    var entityToDto = new CardsDto()
                    {
                        CardNumber = cardsList[i].CardNumber,
                        FullName = cardsList[i].FullName,
                        ExpDate = cardsList[i].ExpDate,
                    };

                    cardsResult.Add(entityToDto);
                }

                return cardsResult;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
