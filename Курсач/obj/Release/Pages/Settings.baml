   M S B A M L   `   `   `  ���� G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Курсач.PagesКурсач.Pages  !    Курсач.Pages.Settings   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  ,local clr-namespace:Курсач.Pages    N� Title$  Settings��   	PageStyle#���   .���   GridMainStyle#���  $��0, 0, 18, 0q� � RowDefinitions �� $��50 ��� $	��100 ��� $	��100 ��� $	��100 ��� $	��100 ��� $��1* � � ColumnDefinitions �� $��1* ��� $	��400 ��� $	��400 ��� $��1* �.+��� 
�������$��1��$��1��$��2��$��7���� $��24(�  
m_Settings#���    TextBlockStyle#���  $��0��$��4��$��Tahoma)�$��Center=�$��Center=�� $��1��$��4��$��1��  	GridStyle#���  $��2�� �� �� �� ��  �� �� �� .+���  �� Orientation$ 
Horizontal=�$��0��$��Right=�$��0��.+���   m_ChangeAppTheme#���  $��18(�$��Tahoma)�$��Center=�$��0, 0, 10, 0q�'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Palette=�$��Center=�$��25��$��25���� $	��150��$��16(�$��1��$��0��$��Tahoma)�$��Center=� �� SelectedIndex �� *ThemeChoice+.@���   m_Light#���  ��  
 m_Dark#���  ��   LightButton2#���   	 	m_Confirm#��� 	 $��2��$��0��$
��Left=�$	��150������ *ChangeThemeCommand+�� $ 
Horizontal=�$��1��$��Right=�$��0��.+���  
 m_ChangeAppLanguage#��� 
 $��18(�$��Tahoma)�$��Center=�$��0, 0, 10, 0q�  $ Globe=�$��Center=�$��25��$��25���� $	��150��$��16(�$��1��$��1��$��Tahoma)�$��Center=� �� *	NewLang+.@���   	m_Russian#���  ��   	m_English#���  �� #���  #��� 	 $��2��$
��Left=�$��1��$	��150������ *ChangeLangCommand+�� $ 
Horizontal=�$��2��$��Right=�$��0��.+���   
m_AboutApp#���  $��18(�$��Center=�$��Tahoma)�$��0, 0, 10, 0q�  $ 
InfoCircle=�$��25��$��25��$��Center=���   LightButton#���  $	��150��$��2��$��2��$
��Left=� 
 m_Open#���  ���� *OpenInfoCommand+�� $ 
Horizontal=�$��3��$��Right=�$��0��.+���   	m_ExitApp#���  $��18(�$��0, 0, 10, 0q�$��Center=�$��Tahoma)�  $ ExitRun=�$��25��$��Center=�$��25����  
 m_Exit#���  $��2��$��3��$
��Left=�  
DarkButton#���  $	��150������ *ExitCommand+