namespace IPD.Web.Models.DTO
{
    public class TreeICDDigonosisCode
    {
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public TreeState? State { get; set; }
        public List<TreeICDDigonosisCode> Children { get; set; }

        public TreeICDDigonosisCode()
        {
            Children = new List<TreeICDDigonosisCode>();
        }

        public static List<TreeICDDigonosisCode> BuildTree(List<TreeICDDigonosisCode> list, string parentId = "#")
        {
            var workingList = list
                .Where(q => q.Parent.Equals(parentId))
                .OrderBy(q => q.Text)
                .ToList();

            if (!workingList.Any())
            {
                return new List<TreeICDDigonosisCode>();
            }

            var listTree = new List<TreeICDDigonosisCode>();
            foreach (var treeItem in workingList.Select(item => new TreeICDDigonosisCode
            {
                Id = item.Id,
                Parent = item.Parent,
                Text = item.Text,
                Icon = "false",
                Children = BuildTree(list, item.Id)
            }))
            {
                if (treeItem.Children.Any())
                {
                    treeItem.State ??= new TreeState();
                    treeItem.State.Disabled = true;
                }
                listTree.Add(treeItem);
            }

            return listTree;
        }

        public static List<TreeICDDigonosisCode> BuildTree(List<ICDDigonosisCodeDto> icdDiagnosisCodes)
        {
            var list = icdDiagnosisCodes.Select(item => new TreeICDDigonosisCode
            {
                Id = item.DiseaseID.ToString(),
                Parent = item.ParentsID == 0 ? "#" : item.ParentsID.ToString(),
                Text = item.ICDCode + " - " + item.Description,
            }).ToList();

            return list;
        }
    }

    public class TreeState
    {
        public bool? Opened { get; set; }
        public bool? Disabled { get; set; }
        public bool? Selected { get; set; }
    }
}
