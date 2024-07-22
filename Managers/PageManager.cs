using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Managers
{
    
    public class PageManager
    {
        public List<Page> pages = new List<Page>();
        int activePage = 0;
        public void addPage(Page page)
        {
            pages.Add(page);
        }
        public void setActivePage(Page page, Game1 g)
        {
            //TODO destory /deload current page?
            page.Init(g);
            setActivePage(page.id);
        }
        public void setActivePage(int id)
        {
            activePage = id;
        }
        public Page getActivePage()
        {
            foreach(Page page in pages)
            {
                if(page.id == activePage)
                {
                    return page;
                }
            }
            return null;
        }
    }
}
