   M S B A M L   `   `   `  ���� G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Курсач.UserControlsКурсач.UserControls  -    'Курсач.UserControls.AddCreditCard   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:Курсач.UserControls     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     >�Name$  addCreditCard��.���    GridMainStyle#���    � ColumnDefinitions �� $��40 ��� $
��0.2* ��� $��1* ��� $
��0.2* ��� $��40 � � RowDefinitions �� $��40 ��� $
��3.5* ��� $��1* ��� $��40 �.+��� ���#B56F2F$��5���� $��0��$��0q�$
��Auto��$
��Auto��  	RedButton#���  $��0������ *CloseUserPageCommand+.��'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 	ArrowBack=�
�������$��30��$��30����   m_AddCreditCard#���  
�������$��1��$��Center=�$��Center=�$��18(�$��3���� 
�������$��2��$��0, 25, 0, 0q� �� CornerRadius$ 15��$��1��._��  �� $��1.25* ��� $��1* ��� $��1* ��� $��1* � �� $��1* ��� $
��2.6* ��� $
��2.6* ��� $��1* �.+��� $��1��$��2��$��Right=�.+���   m_CardNumber#���  $��Tahoma)�$	��0 5q�$��14(��� $	��150�����#F4F2EF  MaterialDesignTextBox#���    
m_16digits)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  $��0, 0, 0, 12q� �� 	MaxLength$ 16������ *CREDIT_CARD+��   ErrorMessage#���  ���� *CardNumberValidation+�� $��2��$��0��$��2��$��Bottom=�$��
0, 0, 0, 7q� �� Orientation$ Vertical=�$��Right=�.+��� $ 
Horizontal=�.+���   
m_Validity#���   �� TextAlignment$ Right=�$	��Top=�$��80��$
��Wrap=�$��0, 0, 10, 0q�$��Tahoma)�$��14(��� $��60��#���    m_MM/YY# �  ���#F4F2EF$��25��$��Bottom=����� *
Validity+�� #���  ���� *ValidityValidation+�� $��2��$��2��$��2��$��Bottom=�$��Center=�.+��� $��CVC/CVV��$��Tahoma)�$��14(�$��Center=�$	��0 5q��� $��60��#���  ���#F4F2EF$ 3��$��0, 0, 0, 12q� 	 	m_3digits# � 	 ���� *CVV+�� #���  ���� *CVVValidation+]� $��2��$��Stretch=�$��1��$��3��.+���  
 LightButton#��� 
 $	��200��$��50��$��Bottom=�  	m_AddCard#���  $��16(����� *AddCardCommand+��   
DarkButton#���  $	��200��$��50��$��Right=�$��Bottom=�  m_Remove#���  $��16(����� *RemoveCardCommand+