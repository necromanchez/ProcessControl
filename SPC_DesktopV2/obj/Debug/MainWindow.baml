   M S B A M L   `   `   `  ����H  DSPC_DesktopV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:SPC_DesktopV2SPC_DesktopV2  5   	       SPC_DesktopV2.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:SPC_DesktopV2     5   	     � WindowStyle$
  None=�5   	    � AllowsTransparency . 
������  � Title$ 
MainWindow��5   	   $	��400��6   $	��800��6*    � 
ResizeMode$ NoResize=�66    � WindowStartupLocation$ CenterScreen=�6L    � ShowInTaskbar . 
�������.��5      � 
������ .+�5   
   ��  �� RadiusX$ 15��6    �� RadiusY$ 15��6!   $��95,10,106,0q�6.   ��5      �� 5      -   5        myImage�� �� ImageSource$ Resources/bg2.jpg��5   .   5      �� 5   
   $��Resources/SPCLogo.png��6   $��140,28,123,157q�6/   �� 5   
   -   5        	 ��Name$	 StartInspection��5      $	��Top=�6*   $
��Left=�6B   ���	#FF28BF3E$��274,320,0,0q�6t   $	��270��6�   $��44��6�   .��5      �� 
 �� Orientation$
 
Horizontal=�6   $��0q�62   .+�5      �� 
�������$��20��60   $��36��6<   �� 6H   $�LOGIN��6L   $��Black"�6Y   6o   5      5      �� 5   
   -   5        $��invalid��5   7   $"��Invalid Username or Password��6   ���	#FFF55C5C$��13(�6]   $
��Bold"�6k   $��306,372,256,0q�6}   � 5!   
    � RowDefinitions 5"      �� 5#      $
��Auto �6    .+�5%      �� -   5        $��UserName��5%      
���   �$��252,234,236,-220q�69    J� RenderTransformOrigin$ 0.5,0.5&�6S   $��25(�6s   $
��Bold"�6�   � 5&      $��533,234,236,-235q�6   .+�5'      �� $��	0,-2,6,10q�6   $��Resources/user.png��5(      $��20��5)      $
��Auto��5*      $��Right=�5+      5,      G� 50      -   5         G�Name$ Password��50       G� PasswordChar$ *��6,   $��252,279,236,-261q�6=   $��25(�6W   $
��Bold"�6e   � 51      $��533,281,236,-282q�6   .+�52      �� $��	0,-2,6,22q�6   $��Resources/download.png��53      $��20��54      $
��Auto��55      $��Right=�56      57      58      5<      5?      