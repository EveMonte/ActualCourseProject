   M S B A M L   `   `   `  ���� G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Курсач.UserControlsКурсач.UserControls  7 clr-namespace:Курсач.PagesКурсач.Pages  '    !Курсач.Pages.Advertisements   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     @userControls'clr-namespace:Курсач.UserControls     2local clr-namespace:Курсач.Pages     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    N� Title$  Advertisements��.���  � RowDefinitions �� $��50 ��� $��1* ��� $��50 �.+��� $��24(�$��Tahoma)�$��Center=�$��Center=�   m_Advertisement#���   �� -    ��Name$ listOfBooks��$��1��$��Disabled=����� *Ads+ �� Mode$ TwoWay=����� .R��� ._�-   'Курсач.UserControls.Advertisement  $	��220��$
��1300������ .R��� $
��0 15q� �� Orientation$ Vertical=�$��Center=��� ���#B56F2F$��2���� $��2��$��Disabled=����� .R�� $��Center=�.@��� .���� 
������ 
������ $��0q����� *AddAdvCommand+.���� $ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon     Kind$	 Add=�$
��Auto��$
��Auto����  	 m_Add#���  $	��5 0q�$��Center=��� .���� 
������ 
������ $��0q����� *GetAdsCommand+.���� $ 
Horizontal=�.+�  $ TrayArrowDown=�$
��Auto��$
��Auto����   	m_GetData#���  $	��5 0q�$��Center=��� .���� 
������ 
������ $��0q����� *SetAdsCommand+.���� $ 
Horizontal=�.+�  $ TrayArrowUp=�$
��Auto��$
��Auto����   
m_SendData#���  $	��5 0q�$��Center=�