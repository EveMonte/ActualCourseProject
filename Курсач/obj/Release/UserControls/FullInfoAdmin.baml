   M S B A M L   `   `   `  ���� G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Курсач.UserControlsКурсач.UserControls  -    'Курсач.UserControls.FullInfoAdmin   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:Курсач.UserControls     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  .���   � RowDefinitions  �� $	��580 ��� $��60 ���  � ColumnDefinitions �� $��1* ��� $	��900 ��� $��1* �.+�� $��0��$��1��   	GridStyle#���    � DataContext �� *CurrentBook+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� $��1* ��� $
��1.4* �  �� $
��3.5* ��� $��1* ��� $
��1.5* ��� $
��0.8* ��� $
��1.2* ��� $��1* �.+��� $
��Left=�$	��Top=�$��0��$��0��$��7������ *COVER+�� $-��'/Media/ЛентаАбонемент.png��$
��Left=�$	��Top=� J� Opacity �� *Subscription+�� $��0��$��1��$��Stretch=�.+���  �� Orientation$ 
Horizontal=�$
��Left=�.+��� $��16(�$��15, 5, 10, 0q�$��Center=�  m_BookTitle#���  �� $��16(�$��
0, 5, 0, 0q�$��Center=� �� TextWrapping$
 Wrap=�$	��330�� �� 	MaxLength$ 65������ *TITLE+�� $	��200��$
��Left=�$��0q�$��15, 20, 0, 0q�$��Bottom=�$��16(�	 �� MaxDropDownHeight$		 300��
 �� SelectedItem
 �� *DataContext.SelectedGenre+ �� RelativeSource ��  �� AncestorType" >�$ TwoWay=����� *DataContext.Genres+ �� " >����� .R�]� .+��� $��
0, 0, 5, 0q�$��Bottom=�$
��Left=�$
��Left=����� *GENRE+�� $��0q�$��Right=�$��Bottom=�$��Right=�$��Bottom=�
������ $��25��$��25��
������ ���� * DataContext.RemoveGenreCommand+ �� " >����� *
GENRE_ID+.��'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Delete=�$��20��$��20��
���   �]� $��15, 0q�.+��� $��16(�$	��200��)  #MaterialDesignThemes.Wpf.HintAssist   Hint$( "Введите новый жанр�� % !MaterialDesignFloatingHintTextBox#���  ���� *DataContext.NewGenre+ �� " >�$ TwoWay=���   LightButton2#���  $��Right=�$��Right=�$��Bottom=�$
��25 0q�$	��200��$��Добавить жанр������ * DataContext.AddNewGenreCommand+ �� " >��� $��15, 5, 0, 0q�$ 
Horizontal=�$
��Left=�.+��� $��16(�$��0, 0, 10, 0q�$��Bottom=�  m_Author#���  �� $��16(�$ 50��$	��200������ *AUTHOR+��  �� SelectedValuePath$ Content��$
��25 0q�$��16(�$	��200�� �� SelectedValue �� *
CATEGORY+$ TwoWay=�.@���   m_Subscription#���  �� $��Покупка��]� $��
0, 5, 0 ,0q�.+��� $��Center=�$��15, 7.5, 0, 7.5q�$
��Left=�$ 
Horizontal=�$��Stretch=�.+��� $��16(�$��
0, 0, 5, 0q�$��Center=����� *RATING+  $��
0, 0, 5, 0q�$
 Star=�$��Center=��� $��16(�$��
0, 0, 5, 0q�$��Center=����� *NUMBEROFVOICES+�� $��Цена��$��16(�$
��10 0q�$��Center=�$��Tahoma)��� $��Tahoma)�$��16(����� *PRICE+�� $��16(�$
 Wrap=�$��Center=�$
��15 0q�$��1��$	 800��$��2��$��2������ *DESCRIPTION+��   LightButton#���  $%��Изменить обложку��$
��15 0q�$��3��$��2��$	��200������ * DataContext.ChangeCoverCommand+ �� " >��� $��16(�$
 Wrap=�$��Center=�$
��15 0q�$��4��$	 150��$��1��$" Ссылка на книгу��$��2������ *LINK+�� $��5��$��2��$��2��$ 
Horizontal=�$��Center=�$��50��$��0, 0, 0, 15q�.+��� #���  $��15, 7, 0, 0q�$	��200��$
��Auto��  	m_Confirm#���  ���� *DataContext.ConfirmCommand+ �� " >���   
DarkButton#���  $��15, 7, 0, 0q�$	��200��$��Удалить��$
��Auto������ *DataContext.RemoveCommand+ �� " >��� $#��Книга в корзине:��$��20(�$��Tahoma)�$��Center=�$��Center=� �� TextAlignment$ Center=�$��3��$��1���� -    ��Name$ listOfBooks��$��2��$��3��$��Disabled=����� *Users+$ TwoWay=�
 �� *SelectedUser+���� .R��� ._�$   Курсач.UserControls.User  $	��400��$	��300�� J� InputBindings d� &   System.Windows.Input.MouseAction   LeftDoubleClick d� MouseAction# Z  �� Command ��  �� $ ListBoxL� �� Path$ DataContext.OpenFullInfo����� .R�� $��Center=�