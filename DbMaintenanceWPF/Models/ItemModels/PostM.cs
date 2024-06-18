using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Repositories;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models.ItemModels
{
    public class PostM(PostRepository postR)
    {
        readonly PostRepository PostR = postR;
        public ReadOnlyObservableCollection<Post> PublicListPosts => new(new ObservableCollection<Post>(PostR.GetAll()));

        public void Create(Post post) => PostR.Add(post);
        public void Update(Post post) => PostR.Update(post.Id, post);
        public void Remove(Post post) => PostR.Remove(post);
    }
}
