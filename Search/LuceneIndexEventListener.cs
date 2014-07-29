using System;
using NHibernate.Event;

namespace AIMS.Search
{
    [Serializable]
    public class LuceneIndexEventListener : IPostUpdateEventListener, IPostDeleteEventListener, IPostInsertEventListener
    {

        /// <summary>
        /// Updates the Full-Text index on the entity.
        /// </summary>
        /// <param name="e"></param>
        public void OnPostUpdate(PostUpdateEvent e)
        {
            LuceneFullTextSearch.UpdateIndexOnEntity(e.Entity);
        }

        /// <summary>
        /// Removes Full-Text Indices on deleted entities.
        /// </summary>
        /// <param name="e"></param>
        public void OnPostDelete(PostDeleteEvent e)
        {
            LuceneFullTextSearch.DeleteIndexForEntity(e.Entity);
        }

        /// <summary>
        /// Adds a Full-Text index on the entity.
        /// </summary>
        /// <param name="e"></param>
        public void OnPostInsert(PostInsertEvent e)
        {
            LuceneFullTextSearch.InsertIndexOnEntity(e.Entity);
        }
    }
}