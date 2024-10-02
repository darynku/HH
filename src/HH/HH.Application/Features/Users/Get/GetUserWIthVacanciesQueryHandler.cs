using HH.Application.DTO;
using HH.Common.Contracts.Handlers;

namespace HH.Application.Features.Users.Get
{
    public class GetUserWIthVacanciesQueryHandler : IQueryHandler<GetUserWithVacanciesQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserWIthVacanciesQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUserWithVacanciesQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAllUserWithVacanciesAsync(request.Page, request.PageSize, cancellationToken);
            return result;
        }
    }
}
