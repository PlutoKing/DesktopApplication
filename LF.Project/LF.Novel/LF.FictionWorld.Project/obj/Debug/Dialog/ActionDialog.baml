   M S B A M L   `   `   `  ����R  NLF.FictionWorld.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:LF.FictionWorld.Project.DialogLF.FictionWorld.Project.Dialog  5   	   1    +LF.FictionWorld.Project.Dialog.ActionDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >local,clr-namespace:LF.FictionWorld.Project.Dialog     5   	   -   5          �Name$  dialog��5	       � Title$ ActionDialog��5   	   $	��450��6   $	��800��6+      	BackColor#��[  5	   !    � WindowStartupLocation$ CenterScreen=�5
   	    � WindowStyle$
 None=�6.    � 
ResizeMode$ NoResize=�6A    � UseLayoutRounding . .��5      �� 
�������$	��0.5q�6    ._�5   
   �  � RowDefinitions 5      �� 5      $��40 �6    �� 5      �� 5      $��40 �6    .+�5      ��   	LineColor#��[ 6   $��0 0 0 1q�6>   ._�5      � -   5         �Name$ TopGrid��5      
������� � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� $��Center=�6O   $��8 0 8 0q�6j     	MainColor#��[ 5      $��16(�6E   $
��Bold"�6S   ��5       �� 	 �� ElementName$	 dialog��
 �� Path$
 Title�5   g   �� 5      -   5         ��Name$ BtnClose��5      $��1��5      $��关闭��6-   $��0 0 8 0q�6:     
FlatButton#��[ 5      $��Center=�5      $��Right=�68   5   H   5      5!      � 5"      $��4q�6   $��1��6    5#      �� 5$      $
��Auto �6$   �� 5%      .+�5'      � $��4q�6    5(      �� 5)      $
��auto �6+   �� 5*      �� 5+      $��30 �6+   �� 5,      $
��auto �6+   �� 5-      .+�5/      �� $��0��6    $��姓名��60   $��Center=�6:   $��0 0 10 0q�6U   6i   �� 50      -   5         ��Name$ CmbName��50      $��1��60    �� DisplayMemberPath$
 Name��6_   $��Center=�6x   $��Center=�6�    �� 
IsEditable . $��28��51   *   $��14(�66    �� SelectedItem 50   @   �� *Role+51   F   �� 52      $��3��6    $��类别��60   $��Center=�6:   $��0 0 10 0q�6U   6i   �� 53      -   5        $��CmbType��53      $��4��6.   $ Value��6\   $��Center=�6v   $��Center=�6�    . $��28��54   *   $��14(�66    53   >   �� *Type+54   F   55      � 56      $��1��6   $��4q�6$    57      �� 58      $
��auto �6+   �� 59      .+�5;      �� $��0��6    $��内容��60   $	��Top=�6:   $��
0 6.4 10 0q�6R   6h   �� 5<      $��1��6   $��0 5.4 0 5.4q�6G    �� TextWrapping$
 Wrap=�6]   $
��Auto=�6q    �� AcceptsReturn . $��14(�5=   7   ��5<   .   �� *	Content+5=   G   5>      5?      �� 5A      $��2��6   #��[ 6"   $��0 1 0 0q�6K   ._�5B      � 
�������.+�5C      ��  �� Orientation$ 
Horizontal=�6!   $��Right=�6:   .+�5D      �� -   5        $��取消��5D   !   $��60��6.   $��0 0 8 0q�69   $��Center=�6J   6~   �� 5E      -   5        $ BtnOK��5E   e   $��确定��6!   $��60��6.   $��0 0 8 0q�69   $��Center=�6J   6�   5F      5G      5I      5J      5K      5M      