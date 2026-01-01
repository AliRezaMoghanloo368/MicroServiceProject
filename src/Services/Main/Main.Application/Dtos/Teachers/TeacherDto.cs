using Main.Application.Dtos.Common;
using Main.Application.Dtos.Histories;

namespace Main.Application.Dtos.Teachers
{
    public class TeacherDto : EntityBaseDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public List<HistoryDto> Histories { get; set; } = new();
    }
}
