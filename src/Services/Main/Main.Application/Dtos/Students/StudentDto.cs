using Main.Application.Dtos.Common;
using Main.Application.Dtos.Histories;

namespace Main.Application.Dtos.Students
{
    public class StudentDto : EntityBaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        //public string? ProfileImageFileId { get; set; }
        public bool IsActive { get; set; }

        public List<HistoryDto> Histories { get; set; } = new();
    }
}
