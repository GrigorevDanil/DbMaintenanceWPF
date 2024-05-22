using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.ManageDatabase;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models
{
    class PostM
    {
        readonly PostRepository PostR;
        public ReadOnlyObservableCollection<Post> PublicListPosts => new(new ObservableCollection<Post>(PostR.GetAll()));

        public PostM(PostRepository postR) => PostR = postR;

        public void Update(Post post) => PostR.Update(post.Id, post);
        public void Remove(Post post) => PostR.Remove(post);
    }
}
