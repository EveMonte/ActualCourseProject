   M S B A M L   `   `   `  ���� G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Курсач.UserControlsКурсач.UserControls  (    "Курсач.UserControls.YourBook   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  3local'clr-namespace:Курсач.UserControls  .���    	GridStyle#���     � ColumnDefinitions  �� $
��1.5* ��� $��2* ��� $��10 � � RowDefinitions �� $
��1.9* ��� $��2* ��� $��2* ��� $��75 �.+��� $
��Left=�$	��Top=�$��0��$��0��$��4������ *COVER+�� $��0��$��1��$��Stretch=�.+���  �� Orientation$ 
Horizontal=�.+��� $��16(�$��15, 5, 5, 0q�  m_BookTitle#���  �� $��16(�$	��300��$
��Wrap=�$��
0, 5, 0, 0q����� *TITLE+�� $��16(�$��15, 5, 0, 0q����� *Genre+�� $��15, 5, 0, 0q�$ 
Horizontal=�.+��� $��16(�$��
0, 0, 5, 0q�  m_Author#���  �� $��16(����� *AUTHOR+]� .+��� $��Center=�$��15, 7.5, 0, 7.5q�$
��Left=�$ 
Horizontal=�$��Stretch=�.+��� $��16(�$��
0, 0, 5, 0q�$��Center=����� *RATING+'  !MaterialDesignThemes.Wpf.PackIcon  $��
0, 0, 5, 0q�   Kind$
 Star=�$��Center=��� $��16(�$��
0, 0, 5, 0q�$��Center=����� *NUMBEROFVOICES+(  "MaterialDesignThemes.Wpf.RatingBar  -     Name$ rating��$��Center=�$��Right=����#B56F2F   Value �� *Mark+ �� Mode$ TwoWay=��� $ 
Horizontal=�$��Right=�$��Center=�$��Right=�.+��� -    ��Name$ SetMark��  LightButton2#���  $
��15 0q�$	��200��$��Right=�  m_RateTheBook#���  ���� *DataContext.MarkCommand+ �� RelativeSource �� 	 �� AncestorType"	 N����� *	BOOK_ID+��   
DarkButton#���  $��3��$��1��$
��Auto��$��50�� 
 m_Open#���  $��0q�$��18(����� *DataContext.DownloadCommand+ �� "	 N����� *	BOOK_ID+�� $��16(�$
��Wrap=�$	��Top=�$��1��$��15, 5, 10, 0q�$��2��$��1������ *DESCRIPTION+