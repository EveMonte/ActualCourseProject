   M S B A M L   `   `   `  ����G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Курсач.UserControlsКурсач.UserControls  5      /    )Курсач.UserControls.ConfirmPurchase   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:Курсач.UserControls     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5
      �    GridMainStyle#���   6     � DataContext  5      �� *	CurrentVM+ � ColumnDefinitions 5   
   �� 5      $��40 �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��40 �6    � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��2* �6   �� 5      $��1* �6   �� 5      $��40 �6   .+�5   
   �� ���#B56F2F$��0��5      $��4��5      �� 5   
     	RedButton#���  6   $��0��5      $��0q�5      $
��Auto��5      $
��Auto��5      $��0��5!      ��5       �� *CancelCommand+.��5"      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 	ArrowBack=�6&   
�������$��30��5$   &   $��30��5%   &   5&      �� 5'   
     m_ConfirmationOfPurchase#���  6   
�������$��1��5)      $��2��5*      $��Center=�5+      $��Center=�5,      $��18(�5-      $��0��5.      �� 5/   
   $��2��6   $��1��50      $	��200��51      $��Bottom=�52      $��50��53      $��16(�54        m_CancelPurchase#���  55        LightButton#���  57      ��56      �� *CancelCommand+57      �� 58   
   $��2��6   $��2��59      $	��200��5:      $��50��5;      $��16(�5<      $��Bottom=�5=        m_ConfirmPurchase#���  5?        
DarkButton#���  5@      ��5>      �� *
BuyCommand+5@      �� 5A   
   $��1��5B      $��1��5C      $��2��5D      $��18(�5E      $
��Wrap=�5F      $��Center=�5G       �� TextAlignment$ Center=�5H      ��5A      �� *
InfoText+5H      5I      5K      