   M S B A M L   `   `   `  ����R  NLF.FictionWorld.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:LF.FictionWorld.Project.PagesLF.FictionWorld.Project.Pages  5      ,    &LF.FictionWorld.Project.Pages.SitePage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:LF.FictionWorld.Project.Pages     5      -   5        .��5
      � $��4q�6     � RowDefinitions  5   
   �� 5      $��1* �6   �� 5      $��1* �6    � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   �� $��0��6   $��0��6   $��4q�6.      	LineColor#��[  69   $��1q�6b   ._�5      � 
�������  5      �� 5      $��90 �6$   �� 5      .+�5      �� $��0��6   $��Bottom=�6-   $��0 0 0 10q�6H   .+�5      ��   	MainColor#��[ 6;   $��Center=�6c   $��	20 0 20 0q�6~   $��30(�5       $
��Bold"�6.   ��5       �� *	Site.Name+5   B   �� 5      $��Bottom=�6;   
�������$��	20 5 20 0q�6h   $��12(�6{   ��6    �� *	Site.Code+6�   5      � 5       $��1��6   $��
20 0 20 10q�6$    5!      �� 5"      $
��auto �6+   �� 5#        5%      �� 5&      $��30 �6(   �� 5'      $��30 �6(   �� 5(      .+�5*      �� $��0��6    $��0��6-   $��所属势力��6=   $��Center=�6I   $��12(�6d   
�������$��0 0 10 0q�6�   6�   �� 5+      $��0��6    $��1��6-   $��Center=�6d   $��12(�6   ��6=   �� *Site.MasterSect.Value+6�   �� 5,      $��1��6    $��0��6-   $��所属区域��6=   $��Center=�6I   $��12(�6d   
�������$��0 0 10 0q�6�   6�   �� 5-      $��1��6    $��1��6-   $��Center=�6X   $��12(�6s   ��6=   �� *	Site.Area+6�   �� 5.      $��2��6    $��0��6-   $��地点简介��6=   $	��Top=�6I   $��12(�6a   
�������$��	0,6.4,0,0q�6�   6�   �� 5/      $��2��6    $��1��6-   $��0,6.4,0,6.4q�6Z   $��12(�6o   $
��Wrap=�50   0   ��5/   =   �� *
Site.Brief+50   G   52      53      54      � 55   
   $��0��6   $��1��6   $��2��6,   $��4q�6@     56      �� 57      $��30 �6    �� 58      $��1* �6    .+�5:      �� $��0��6   $��0��6"   $��4��62   #��[  5;      $��1 1 1 0q�6:   
�������._�5<      �  5=      �� 5>      $��1* �6+   �� 5?      $
��Auto �6+   .+�5A      ��  �� Orientation$ 
Horizontal=�6!   $��3q�6:   $��Center=�6E   .+�5B      �� $��地点列表��6$   $��Center=�60   #��[ 6K   $��5 0 10 0q�6s   6�   �� 5C      -   5         ��Name$ BtnAdd��5C   !   $��添加��61   $��0 0 5 0q�6>   5D   8   �� 5E      -   5        $ BtnEdit��5E   !   $��编辑��62   $��0 0 5 0q�6?   5F   9   �� 5G      -   5        $ 	BtnDelete��5G   !   $��删除��64   $��0 0 5 0q�6A   5H   ;   �� 5I      -   5        $ BtnUp��5I   !   $��上移��60   $��0 0 5 0q�6=   5J   7   �� 5K      -   5        $ BtnDown��5K   !   $��下移��62   $��0 0 5 0q�6?   5L   9   �� 5M      $	��100��6"   $��0 0 2 0q�6.   $��Center=�6?   6c   �� 5N      $��搜索��6!   $��0 0 0 0q�6.     
FlatButton#��[ 6?   6e   5O      �� 5P      $��1��6!   $ 
Horizontal=�61   $��5q�6J   $��Center=�6U   .+�5Q      �� $��	总计：��6$   61   �� 5R      -   5         ��Name$ TxbCount��5R   $   #��[ 6V   $��0 0 3 0q�6~   ��66   �� *SiteList.Count+6�   5S      5T      5V      &   System.Windows.Controls.DataGrid  5W      -   5          Name$ DtgSites��5W         
IsReadOnly . $��1��6;      SelectionUnit$ FullRow=�5X         Columns 5Y      0  *System.Windows.Controls.DataGridTextColumn  5Z      ,  &System.Windows.Controls.DataGridColumn	   Header$	 序号��6)   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 60 65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� *Code+6[     5[      $	 名称��6)   $
 80 65    6@   �� *Name+6[     5\      $	 所属区域��6)   $
 2* 67    6B   �� *Area+6]     5]      $	 所属势力��6)   $
 * 67    6A   �� *MasterSect.Value+6h   5_      5`      �� 5a   
   $��0��6   $��3��6    $��4q�60   $��区域信息��5b      6#   �� 5c   
   $��1��6   $��0��6    $��4q�60   $��物产��6;   .��5d        -	   5        $ DtgSiteItems��5d      $��0q�6-    .    AutoGenerateColumns .     CanUserAddRows .     HeadersVisibility$ Column=�5e   W   $ FullRow=�6r    5f        5g      $	 序号��6)   $
 * 65      MinWidth$ 60Q�6X    6?   �� *Code+6h     5h      $	 名称��6)   $
 * 65   $ 80Q�6X      
Foreground# [ 6f    6?   �� *Name+6�   5j      5k      �� 5l   
   $��地理信息��6   $��1��6!   $��1��6.   $��2��6>   $��4q�6R   .��5m        -
   5        $ DtgSubSites��5m      $��0��6,   
�������$��0q�6L    .  .   .  $ Column=�5n   W   $ FullRow=�6r    5o        5p      $	 序号��6)   $
 60 65   $ 60Q�6W    6@   �� *ID+6g     5q      $	 名称��6)   $
 80 65   $ 80Q�6Y   # [ 6g    6@   �� *Name+6�     5r      $	 简介��6)   $
 * 65   $ 80Q�6Y    6?   �� *Brief+6i   5t      5u      �� 5v   
   $��1��6   $��3��6    $��4q�60   $��势力��5w      .��5x        -   5        $ DtgSiteSects��5x      $��0q�6-    .  .   .  $ Column=�5y   W   $ FullRow=�6r    5z        5{      $	 序号��6)   $
 * 65    6?   �� *Code+6Z     5|      $	 名称��6)   $
 * 65   # [ 6X    6?   �� *Name+6�   5~      5�      5�      5�      