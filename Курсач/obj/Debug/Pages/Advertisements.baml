   M S B A M L   `   `   `  ����G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Курсач.UserControlsКурсач.UserControls  5      7 clr-namespace:Курсач.PagesКурсач.Pages  5      '    !Курсач.Pages.Advertisements   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      @userControls'clr-namespace:Курсач.UserControls     5      2local clr-namespace:Курсач.Pages     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  60     N� Title$  Advertisements��5	      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��1* �6   �� 5      $��50 �6   .+�5   
   �� $��24(�6   $��Tahoma)�5      $��Center=�5      $��Center=�5         m_Advertisement#���   5      �� 5   
   -   5         ��Name$ listOfBooks��5      $��1��5      $��Disabled=�5      ��5      �� *Ads+ �� Mode$ TwoWay=���5      �� 5      .R�5      �� ._�5      -   'Курсач.UserControls.Advertisement  $	��220��65   $
��1300��6B   5      5      ��5!      �� 5"      .R�5#      �� $
��0 15q�6!    �� Orientation$ Vertical=�6/   $��Center=�6F   5$      5&      �� 5'   
   ���#B56F2F$��2��5(      �� 5)   
   $��2��6   $��Disabled=�5*      ��5+      �� 5,      .R�5-      � $��Center=�6    5.      .@�50      �� .��51      �� 
������ 
������ $��0q�54      ��53      �� *AddAdvCommand+.��55      �� $ 
Horizontal=�6!   .+�56      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$	 Add=�62   $
��Auto��57   .   $
��Auto��58   .   �� 59       	 m_Add#���  6$   $	��5 0q�5:   $   $��Center=�5;   $   5<      5=      5>      �� 5?      .��5@      �� 
������ 
������ $��0q�5C      ��5B      �� *GetAdsCommand+.��5D      �� $ 
Horizontal=�6!   .+�5E        $ TrayArrowDown=�62   $
��Auto��5F   .   $
��Auto��5G   .   �� 5H        	m_GetData#���  6$   $	��5 0q�5I   $   $��Center=�5J   $   5K      5L      5M      �� 5N      .��5O      �� 
������ 
������ $��0q�5R      ��5Q      �� *SetAdsCommand+.��5S      �� $ 
Horizontal=�6!   .+�5T        $ TrayArrowUp=�62   $
��Auto��5U   .   $
��Auto��5V   .   �� 5W        
m_SendData#���  6$   $	��5 0q�5X   $   $��Center=�5Y   $   5Z      5[      5\      5]      5_      5`      