   M S B A M L   `   `   `  ����H  DSPC_DesktopV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:SPC_DesktopV2.InspectionItemSPC_DesktopV2.InspectionItem  5   	       SPC_DesktopV2.Home   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:SPC_DesktopV2.InspectionItem     5   	     � WindowStyle$
  None=�5   	    � AllowsTransparency . 
������  � Title$
 Home��5   	   $	��650��6   $
��1300��6$    � 
ResizeMode$ NoResize=�61    � WindowStartupLocation$ CenterScreen=�6G    � ShowInTaskbar . 
�������.��5      ��  �� CornerRadius$ 35��6   ���	#9913C9FF$��5q�67   ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      � 	 � Color$	 	#FF7EE7FF��6   
 � Offset$
 0Q�61   � 5      $	 	#FFC4E1FF��6   $
 1Q�61   � 5      $	 	#FFA0E4FF��6   $
 0.233Q�61   5      ._�5   
   � $��	96,0,-6,0q�6    � RowDefinitions 5      �� 5      $
��292* �6    �� 5      $	��19* �6    .+�5      � $��-96,0,0,514q�6   .+�5      �� $D��>/SPC_DesktopV2;component/Resources/1280px-Shimano_logo.svg.png��6   $��506,61,528,-61q�6`   �� 5      -   5         ��Name$	 PCS��5   *   $
��Bold"�6   $(��"STATISTICAL PROCESS CONTROL SYSTEM��67   $
��Left=�6e   $��398,148,0,-104q�6�   $	��Top=�6�   $	��526��6�   $��25(�6�   5       � 5!      $��0,0,0,19q�6   .+�5$      �� ���	#9913C9FF$��5q�61   $
��Left=�6E   $��246,223,0,231q�6`   $	��641��6w   $ 	15,15,0,0��6�   ��5%      �� 5&      $ 0.5,1&�6.   $ 0.5,0&�6?   .��5'      � $	 	#FF89E6FF��6+   $
 1Q�6=   � 5(      $	 	#FF001662��6+   � 5)      $	 	#FF47C8E4��6+   5*      5,      �� 5.      -   5        $��InspectionScreen��5.   �   $	��Top=�6   $
��Left=�61   ���	#FF00ADC9$��342,258,0,0q�6c   $	��100��6x   $��67��6�   .��5/      ��  �� Orientation$ 
Horizontal=�6!   $��0q�6:   .+�50      �� $6��0/SPC_DesktopV2;component/Resources/73292-200.png��6    $	��113��6Z   $��51��6f   $��0,0,10,0q�6r   51       � ToolTip 52      _� 53      .��54      �� .+�55   "   �� $
��Bold"�6,   Item Inspection6O   �� 56   "   !Create or Continue inspections.6M   57      58      5:      �� 5<      -   5        $��UploadScreen��5<   �   $	��Top=�6   $
��Left=�61   ���	#FF00ADC9$��509,258,0,0q�6c   $	��100��6x   $��67��6�   .��5=      �� $ 
Horizontal=�6!   $��0q�6:   .+�5>      �� $E��?/SPC_DesktopV2;component/Resources/upload-cloud-pngrepo-com.png��6    $��81��6i   $��54��6t   $��0,0,10,0q�6�   5?       5@      _� 5A      .��5B      �� .+�5C   "   �� $
��Bold"�6,   
Upload CMM6J   �� 5D   "   Inspection using CMM Upload6I   5E      5F      5H      �� 5J      -   5        $��LogOffScreen��5J   �   $	��Top=�6   $
��Left=�61   ���	#FF00ADC9$��675,258,0,0q�6c   $	��100��6x   $��67��6�   .��5K      �� $ 
Horizontal=�6!   $��0q�6:   .+�5L      �� $7��1/SPC_DesktopV2;component/Resources/img_426756.png��6    $��80��6[   $��52��6f   $��0,0,10,0q�6r   5M       5N      _� 5O      .��5P      �� .+�5Q   "   �� $
��Bold"�6,   	Log off6G   �� 5R   "   Log off or Switch user6D   5S      5T      5V      5X      � 5[      $��-96,582,895,0q�6   $��2��6*   .+�5]      �� $9��3/SPC_DesktopV2;component/Resources/company_logo.png��6   $��	0,0,174,0q�6V   �� 5_      
���   �$��WrapWithOverflow=�6/   $��232,10,-849,-10q�6O   ��Information appearing on Shimano Process Control System intranet application are copyrighted by Seiko IT Solution Philippines Inc. (SITP). Permission to reprint or electronically reproduce any document or graphic in part or in its entirety for any reason other than personal use is expressly prohibited, unless prior written consent is obtained from the SITP Staff and the proper entities."5a      5b      5c      5d      5e      