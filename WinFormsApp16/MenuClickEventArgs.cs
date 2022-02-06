namespace WinFormsApp16
{
    public class MenuClickEventArgs : EventArgs
    {
        public bool FocusedCenter { get; }
        public int MenuIndex { get; }

        public MenuClickEventArgs(bool focusedCenter, int menuIndex)
        {
            FocusedCenter = focusedCenter;
            MenuIndex = menuIndex;
        }
    }
    
}
