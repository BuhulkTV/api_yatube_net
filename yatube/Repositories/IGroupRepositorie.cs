using yatube.Models;

namespace yatube.Repositories
{
    public interface IGroupRepositorie
    {
        public List<GroupsForGet> GetGroups();
        public GroupForGet GetGroup(int groupId);
        public void AddGroup(GroupsForCreate groups);
        public void RemoveGroup(int groupId);
    }
}
