   M S B A M L   `   `   `  ����R  NLF.FictionWorld.Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:LF.FictionWorld.Project.DialogLF.FictionWorld.Project.Dialog  5   	   0    *LF.FictionWorld.Project.Dialog.WorldDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >local,clr-namespace:LF.FictionWorld.Project.Dialog     5   	   -   5        $��dialog��5	   	     � Title$  WorldDialog��5   	   $	��450��6   $	��800��6*      	BackColor#��[  5	       � WindowStartupLocation$ CenterScreen=�5
   	    � WindowStyle$
 None=�6.    � 
ResizeMode$ NoResize=�6A   
�������$��1q�5   3    � UseLayoutRounding . .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      �� 5      $��40 �6   .+�5   
   �� -   5          	LineColor#��[ 5      $��0 0 0 1q�6:   
�������$��0q�5      ._�5      ��  �� Orientation$ 
Horizontal=�6   $��8 0 8 0q�62   $��Center=�6C   .+�5      �� $��0 0 4 0q�6K   ��6   ��  �� ElementName$ dialog�� �� Path$ Title�6^   �� 5      -   5        	 ��Name$	 TxbSubTitle��5        	MainColor#��[ 61   6[   5      5      �� 5   
   $��1��6   $��0q�6"     LeftTabControl#��[ 66     LeftTabItem#��[ 6^   .@�5      �� $��历  法��6   5      �� 5      $��修  炼��6   5      5       �� 5!   
   $��2��6   #��[ 6   $��0 1 0 0q�6G   
�������._�5"      �� $ 
Horizontal=�6   $��Right=�62   .+�5#      �� -   5        $��取消��5#      $��60��6&   $��0 0 8 0q�61   $��Center=�6B   6v   �� 5$      -   5        
 ��Name$
 BtnOK��5$   ]   $��确定��6   $��60��6&   $��0 0 8 0q�61   $��Center=�6B   6�   5%      5&      5'      5(      