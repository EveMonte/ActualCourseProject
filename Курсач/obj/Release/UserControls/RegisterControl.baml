   M S B A M L   `   `   `  ���� G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:КурсачКурсач  ;"clr-namespace:Курсач.HelpersКурсач.Helpers  "    Курсач.RegisterControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ,localclr-namespace:Курсач     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  3
components"clr-namespace:Курсач.Helpers  .���    	GridStyle#���     � RowDefinitions  �� $
��0.9* ��� $
��0.9* ��� $
��1.1* ��� $
��0.9* ��� $��1* ��� $
��0.6* �.+�� $��Stretch=�$��0�� � ColumnDefinitions �� �� .+��� $��0��$��0��.+��� -    ��Name$ secondNameTextBox��$��50��$��0��$��	18,0,10,0q� �� SelectionBrush
 �  ��$��Stretch=� �� 
CaretBrush
 �  ��  m_SecondNameShort)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �   % !MaterialDesignFloatingHintTextBox#��[ $��16(�
������  �� MaxLines$ 1������ *
SecondName+.����   ErrorMessage#���  $��Center=����� *SecondNameValidation+�� $��0��$��1��.+��� -   $ nameTextBox��$��50��$��1��$��Stretch=�$��	10,0,18,0q�  m_NameShort# �  #��[ $��16(����� *Name+�� #���  $��Center=����� *NameValidation+�� $��1��.+��� -   $ emailTextBox��$��1��$��50��$
��18,0q�$ Email��#��[ $��16(����� *Email+�� $��Center=�#���  ���� *EmailValidation+�� $��2��.+�G� -    G�Name$ password1Box��$��50��$
��18,0q� ) %MaterialDesignFloatingHintPasswordBox#���    
m_Password# �  $��16(�*  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors 5   /Курсач.Helpers.PasswordBoxBindingBehavior  	   Password	 �� *FirstPassword+�� #���  $
��Wrap=�$	��400������ *PasswordValidation+�� $��3��.+�G� -   $ password2Box��$��50��$
��18,0q�#���    m_RepeatPassword# �  $��16(�   	 �� *SecondPassword+�� $��Center=�#���  ���� *SecondPasswordValidation+�� -   
 ��Name$
 Create_Account��$��4��  
DarkButton#���  $
��Auto��$
��15 7q�$��20(� 	 m_CreateAnAccount#��� 	 ���� *RegistrationCommand+�� $��5��.+��� $	��120��$��0q�
������ 
������ ���� *OpenSignInCommand+.����  � Cursor$
 Hand��$��Center=�$��0,0,0,0q� 
 m_AlreadyHaveAnAccount#��� 
 $��14(����	#FF1877F3