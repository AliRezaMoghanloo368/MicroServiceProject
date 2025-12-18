using Main.Application.Dtos.Common;

namespace Main.Application.Dtos.Students
{
    public class StudentDto : EntityBaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        //public string? ProfileImageFileId { get; set; }
        public bool IsActive { get; set; }
    }
}
