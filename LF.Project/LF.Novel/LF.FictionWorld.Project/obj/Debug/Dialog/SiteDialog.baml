   M S B A M L   `   `   `  ����R  NLF.FictionWorld.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:LF.FictionWorld.Project.DialogLF.FictionWorld.Project.Dialog  5   	   S.clr-namespace:LF.FictionWorld.Project.Controls LF.FictionWorld.Project.Controls  5   	   /    )LF.FictionWorld.Project.Dialog.SiteDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >local,clr-namespace:LF.FictionWorld.Project.Dialog     5   	   Ccontrols.clr-namespace:LF.FictionWorld.Project.Controls     5   	   -   5        $��dialog��5
   	     � Title$  
SiteDialog��5	   	   $	��500��6   $	��800��6)    � UseLayoutRounding . 
�������$��1q�5
   C   ���#f0f0f0 � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�6C    � 
ResizeMode$ NoResize=�6V   .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      �� 5      $��40 �6   .+�5   
   ��    	LineColor#��[  6   $��0 0 0 1q�6:   ._�5      � -   5         �Name$ TopGrid��5      
�������.+�5      �� $��Center=�6K   $��8 0 8 0q�6f     	MainColor#��[ 5      ��5      ��  �� ElementName$ dialog�� �� Path$ Title�5   G   5      5      �� 5   
   $��1��6   $��0q�6"     LeftTabControl#��[ 66     LeftTabItem#��[ 6^   .@�5      �� $��信息��6   .��5      � $��4q�6    5      �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   �� 5      .+�5!      � $��4q�6   	 � ColumnDefinitions	 5"      �� 5#      $
��auto �6/   �� 5$      .+�5&      �� $��0��6$   $��名称��64   $��Center=�6>   $��0 0 10 0q�6Y   6m   �� 5'      $��1��6"   $��Center=�6H   $��Center=�6c   $��28��6�   ��62   �� *Name+6�   5(      � 5)      $��1��6   $��4q�6(   	 5*      �� 5+      $
��auto �6/   �� 5,      �� 5-      �� 5.      .+�50      �� $��0��6$   $��区域��64   $��Center=�6>   6[   �� 51      -   5        
 ��Name$
 CmbAdd1��52   +   $��1��51   #   $��28��63    �� 
IsEditable . $��10 0 0 0q�6Q   $��Center=�6c    �� DisplayMemberPath$ Value��52   <    �� SelectedItem 6V   �� *Area1+53   Y   �� 54      -   5        $
 CmbAdd2��55   +   $��2��54   #   $��28��63    . $��10 0 0 0q�6Q   $��Center=�6c   $ Value��55   <    6V   �� *Area2+56   Y   �� 57      -   5        $
 CmbAdd3��58   +   $��3��57   #   $��28��63    . $��10 0 0 0q�6Q   $��Center=�6c   $ Value��58   <    6V   �� *Area3+6w   59      � 5:      $��2��6   $��4q�6(   	 5;      �� 5<      $
��auto �6/   �� 5=      .+�5?      �� $��0��6$   $��简介��64   $	��Top=�6>   $��
0 6.4 10 0q�6V   6l   �� 5@      $��1��6"   $��0 5.4 0 5.4q�6I    �� TextWrapping$
 Wrap=�6_   $
��Auto=�6s   ��62   �� *Brief+6�   5A      5B      5C      �� 5D      $��地理��6   .��5E      �  5F      �� 5G      $
��auto �6(   �� 5H      .+�5J      ��  �� Orientation$ 
Horizontal=�6!   $��8 8 8 0q�6:   .+�5K      �� -   5         ��Name$ 	BtnAddSub��5K   !   $��添加��64   $��60��6A   6f   �� 5L      -   5        $ BtnDeleteSub��5L   !   $��删除��67   $��60��6D   $��8 0 0 0q�6O   6}   5M      &   System.Windows.Controls.DataGrid  5N      -   5          Name$ DtgSubSites��5N      $��1��64   $��8q�6A   
�������   AutoGenerateColumns .     CanUserAddRows .     HeadersVisibility$ Column=�5O   W      SelectionUnit$ FullRow=�6r      Columns 5P      0  *System.Windows.Controls.DataGridTextColumn  5Q      ,  &System.Windows.Controls.DataGridColumn   Header$ 序号��61      Width5  /System.Windows.Controls.DataGridLengthConverter$ 60 6=   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *ID+6a     5R      $ 名称��61   $	 100 6=      
Foreground �#1D6BFF 6I   �� *Name+6y     5S      $ 简介��61   $ * 6=    6G   �� *Brief+6c   5U      5V      5W      �� 5X      $��物产��6   .��5Y      � $��8q�6   	 5Z      �� 5[      �� 5\      $
��Auto �6+   �� 5]      .+�5_      �� -	   5         ��Name$ ListAllItems��5_      $
 Name��65   $��1q�6N   6d   �� 5`      $��1��6!   $��Center=�61   .+�5a      �� -
   5        $ 
BtnAddItem��5a   !   $��添加��65   $��60��6B   $��10q�6M   6t   �� 5b      -   5        $ BtnRemoveItem��5b   !   $��移除��68   $��60��6E   $��10q�6P   6z   5c      �� 5d      -   5        $ 	ListItems��5d   .   $��2��6   $
 Name��6B   $��1q�6[   6q   5e      5f      �� 5g      $��势力��6   .��5h      � $��8q�6   5i      5j      5k      �� 5l   
   $��2��6   $��0 1 0 0q�6   #��[  68   ._�5m      � 
�������.+�5n      �� $ 
Horizontal=�6   $��Right=�66   .+�5o      �� -   5        $��取消��5o      $��60��6*   $��0 0 8 0q�65   6_   �� 5p      -   5        $ BtnOK��5p   F   $��确定��6   $��60��6*   $��0 0 8 0q�65   6k   5q      5r      5s      5t      5u      