   M S B A M L   `   `   `  ����R  NLF.FictionWorld.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:LF.FictionWorld.Project.DialogLF.FictionWorld.Project.Dialog  5   	   S.clr-namespace:LF.FictionWorld.Project.Controls LF.FictionWorld.Project.Controls  5   	   /    )LF.FictionWorld.Project.Dialog.BookDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >local,clr-namespace:LF.FictionWorld.Project.Dialog     5   	   Ccontrols.clr-namespace:LF.FictionWorld.Project.Controls     5   	   -   5        $��dialog��5
   	     � Title$  
BookDialog��5	   	   $	��500��6   $	��800��6)      	BackColor#��[  5
       � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�6.    � 
ResizeMode$ NoResize=�6A   
�������$��1q�5   3    � UseLayoutRounding . .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      �� 5      $��40 �6   .+�5   
   �� -   5          	LineColor#��[ 5      $��0 0 0 1q�6:   
�������$��0q�5      ._�5      ��  �� Orientation$ 
Horizontal=�6   $��8 0 8 0q�62   $��Center=�6C   .+�5      �� $��0 0 4 0q�6K   ��6   ��  �� ElementName$ dialog�� �� Path$ Title�6^   �� 5      -   5        	 ��Name$	 TxbSubTitle��5        	MainColor#��[ 61   6[   5      5      �� 5   
   $��1��6   $��0q�6"     LeftTabControl#��[ 66     LeftTabItem#��[ 6^   .@�5      �� $��信息��6   .��5      � $��4q�6   
 � ColumnDefinitions
 5      �� 5      �� 5      $	��100 �6+   .+�5!      �  5"      �� 5#      $
��Auto �6,   �� 5$      .+�5&      � $��4q�6   
 5'      �� 5(   "   $
��auto �63   �� 5)   "   �� 5*   "   $��30 �63   �� 5+   "   $
��auto �63   �� 5,   "   �� 5-   "   $��30 �63   �� 5.   "   $
��auto �63   �� 5/   "   .+�51      �� $��0��6(   $��名称��68   $��Center=�6B   $��0 0 10 0q�6]   6q   �� 52      $��1��6&   $��Center=�6L   $��Center=�6g   $��28��53   *   ��52   6   �� *Name+53   8   �� 54      $��3��6(   $��等级��68   $��Center=�6B   $��0 0 10 0q�6]   6q   �� 55      -   5        $��CmbLevel��55   '   $��4��67    �� DisplayMemberPath$ Value��6f   $��Center=�6�   $��Center=�6�    �� 
IsEditable . $��28��56   +    �� SelectedItem 55   G   �� *Level+56   9   �� 57      $��6��6(   $��类别��68   $��Center=�6B   $��0 0 10 0q�6]   6q   �� 58      -   5        $��CmbType��58   '   $��7��66   $ Value��6d   $��Center=�6~   $��Center=�6�    . $��28��59   +    58   F   �� *Type+59   9   5:      � 5;      $��1��6   $��4q�6,   
 5<      �� 5=   "   $
��auto �63   �� 5>   "   .+�5@      �� $��0��6(   $��简介��68   $	��Top=�6B   $��
0 6.4 10 0q�6Z   6p   �� 5A      $��1��6&   $��0 5.4 0 5.4q�6N    �� TextWrapping$
 Wrap=�6d   $
��Auto=�6x   ��67   �� *Brief+6�   5B      5C      8   2LF.FictionWorld.Project.Controls.AttributeSelector  5D      -   5          Name$ roelas��5D   1   $��1��6A   $��4q�6Q      
Attributes 6\   �� *
Attributes+ �� Mode$ TwoWay=�6�   5E      5F      �� 5G      $��内容��6   .��5H      �  5I      �� 5J      $
��auto �6(   �� 5K      .+�5M      �� $ 
Horizontal=�6!   $��8 8 8 0q�6:   .+�5N      �� -   5         ��Name$ BtnAddLevel��5N   !   $��添加��66   $��60��6C   6j   �� 5O      -   5        $ BtnDeleteLevel��5O   !   $��删除��69   $��60��6F   $��8 0 0 0q�6Q   6�   �� 5P      -	   5        $ BtnTemplate��5P   !   $��选择模板��66   $��60��6E   $��8 0 0 0q�6P   6c   5Q      &   System.Windows.Controls.DataGrid  5R      -
   5          Name$ 
DtgContent��5R      $��1��63   $��8q�6@   $   HorizontalScrollBarVisibility$
 Auto=�6K      Columns 5S      0  *System.Windows.Controls.DataGridTextColumn  5T      ,  &System.Windows.Controls.DataGridColumn   Header$ 序号��61      Width5  /System.Windows.Controls.DataGridLengthConverter$ 60 6=      MinWidth$ 60Q�6_   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *ID+6o     5U      $ 名称��61   $	 100 6=   $ 80Q�6b      
Foreground �#1D6BFF 6I   �� *Name+6�     5V      $ 简介��62   $ * 6>   $ 80Q�6b    6H   �� *Brief+6r   5X      5Y      5[      �� 5\      $��技能��6   .��5]      �  5^      �� 5_      $
��auto �6(   �� 5`      .+�5b      �� $ 
Horizontal=�6!   $��8 8 8 0q�6:   .+�5c      �� -   5        $ BtnAddSkill��5c   !   $��添加��66   $��60��6C   6j   �� 5d      -   5        $ BtnDeleteSkill��5d   !   $��删除��69   $��60��6F   $��8 0 0 0q�6Q   6e   5e        5f      -   5        $ 	DtgSkills��5f      $��1��62   $��8q�6?   $
 Auto=�6J    5g        5h      $ 序号��61   $ 40 6=    6H   �� *ID+6b     5i      $ 名称��61   $ 80 6=    �#1D6BFF 6H   �� *Name+6x     5j      $ 所需等级��61   $ 40 6?    6J   �� *Level+6f     5k      $ 修炼前提��61   $ 40 6?    6J   �� *Precondition+6n     5l      $ 简介��61   $ * 6=    6G   �� *Brief+6d   5n      5o      5p      5q      �� 5r   
   $��2��6   #��[ 6   $��0 1 0 0q�6G   
�������._�5s      �� $ 
Horizontal=�6   $��Right=�62   .+�5t      �� -   5        $��取消��5t      $��60��6&   $��0 0 8 0q�61   $��Center=�6B   6v   �� 5u      -   5        $ BtnOK��5u   ]   $��确定��6   $��60��6&   $��0 0 8 0q�61   $��Center=�6B   6�   5v      5w      5x      5y      