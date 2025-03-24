namespace UniversityAPI.DTO
{
    public class AllDeptsWithStdNamesDTO
    {
        public string DeptName { get; set; }
        public string deptLocatin{ get; set; }
        public List<string> StudentNames { get; set; }
        public int StudentCount { get; set; }
        public string Message { get; set; }
    }
}
