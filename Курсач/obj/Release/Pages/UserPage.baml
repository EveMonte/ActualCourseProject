   M S B A M L   `   `   `  ���� G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Курсач.PagesКурсач.Pages  ;"clr-namespace:Курсач.HelpersКурсач.Helpers  !    Курсач.Pages.UserPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  ,local clr-namespace:Курсач.Pages  _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  3
components"clr-namespace:Курсач.Helpers    N� Title$  UserPage�� N� 
Background �#F4F2EF.��� $��0, 0, 18, 0q� � RowDefinitions �� $��50 ��� $��1* ��� $
��1.9* ��� $��1* ��� $��1* ��� $��1* ��� $��1* � � ColumnDefinitions �� $��1* ��� $	��400 ��� $	��400 ��� $��1* �.+��� 
�������$��1��$��1��$��2��$��7���� $��24(�   m_UserAccount#���   $��0��$��4��$��Tahoma)�$��Center=�$��Center=�� $��1��$��2��$��1��$��Stretch=�$��2�� �� �� �� �� �� �� �� ��  �� $
��1.5* ��� $��1.25* ��� $
��1.5* �.+�'  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 User=�$��0��$��3��$��Stretch=�$��Stretch=�$	��100��$	��100����   m_SecondName#���  $��1��$��0��$��Right=�$��0, 0, 15, 0q�$��16(��� $��1��$��1��$��16(����� *
SECONDNAME+��  
 m_Name#���  $��2��$��0��$��Right=�$��0, 0, 15, 0q�$��16(��� $��1��$��2��$��16(����� *NAME+��   	m_SignOut#���  $	��220��$
��Left=�$
��10 0q�$��Bottom=�$��2��$��1��  
DarkButton#���  ���� *SignOutCommand+��   m_SubscriptionStatus#���  $��16(�$��Right=�$��3��$��0, 0, 15, 0q�$��Bottom=�$��0���� $��1��$��3��$��Bottom=�$��16(����� *Subscription+ �� Mode$ TwoWay=���   m_CardNumber2#���  $��Right=�$��Bottom=�$��0��$��0, 0, 15, 0q�$��4��$��16(��� $��4��$��Bottom=�$
��Left=�$��1��$��16(����� *
CreditCard+$ TwoWay=���   m_Change#���    LightButton2#���  $��2��$��4��$	��220��$
��10 0q�$
��Left=�$��Bottom=����� *ApplyCreditCardCommand+��  	 m_NumberOfBooksOnTheShelf#��� 	 $��0��$��5��$��0, 0, 15, 0q�$��Bottom=�$��Right=�$��16(��� $��5��$��Bottom=�$��1��$��16(����� *	YourBooks+$ TwoWay=���  
 m_NumberOfMarks#��� 
 $��0��$��Bottom=�$��6��$��0, 0, 15, 0q�$��Right=�$��16(��� $��6��$��Bottom=�$��1��$��16(����� *Marks+$ TwoWay=��� $��3��$��1��$��4��$��2��$	��Top=�$��Stretch=�.+�9�   m_ChangeEmail#���  |�.  
�������.���  �� $
��1.5* ��� $��1.25* ��� $
��1.5* � �� �� .+���   m_EnterNewEmail#���  $��16(�$��0��$��0��$��Right=�$��Bottom=�$��0, 0, 15, 0q��� $��1��$��0��$��16(�$
��Left=�$	��220��  MaterialDesignTextBox#���  ���� *
NewEmail+$ TwoWay=���   m_SendCodeToMail#���  #���  $
��10 0q�$	��220��$��2��$
��Left=�$��0������ *SendEmailMessageCommand+��   m_EnterConfirmationCode#���  $��0��$��Bottom=�$��Right=�$��16(�$��0, 0, 15, 0q�$��1���� $��16(�$��1��$��1��$
��Left=�$	��220��$��40��#���  ���� *Code+$ TwoWay=��� #���    	m_Confirm#���  $	��220��$��0q�$
��10 0q�$
��Left=�$��Bottom=�$��2��$��1������ *ApplyEmailCommand+9�   m_ChangePassword#���  
�������.��� -    �Name$ passwordStk��$��1��$��4��$��2��$��2�� �� �� �� �� �� ��  �� $
��1.5* ��� $��1.25* ��� $
��1.5* �.+���   m_EnterOldPassword#���  $��16(�$��Bottom=�$��Right=�$��0��$��0��$��0, 0, 15, 0q�G� $	��220��$��0��$��40��$��1��$��Bottom=�$
��Left=�$��16(�*  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors 5   /Курсач.Helpers.PasswordBoxBindingBehavior     Password �� *OldPassword+��   m_EnterNewPassword#���  $��16(�$��1��$��0��$��Right=�$��Bottom=�$��0, 0, 15, 0q�G� $	��220��$��1��$��1��$
��Left=�$��Bottom=�$��40��$��16(�    �� *FirstPassword+��   m_RepeatNewPassword#���  $��16(�$��2��$��0��$��Right=�$��Bottom=�$��0, 0, 15, 0q�G� $	��220��$��Bottom=�$��1��$��2��$��40��$��16(�$
��Left=�    �� *SecondPassword+�� -   	 ��Name$	 PasswordBtn��#���  #���  $	��220��$
��10 0q�$
��Left=�$��Bottom=�$��2��$��2������ *SendPasswordMessageCommand+���� �� #���  $��0��$��Bottom=�$��Right=�$��16(�$��0, 0, 15, 0q�$��3���� $��16(�$��3��$��1��$
��Left=�$	��220��$��40��#���  ���� *Code2+$ TwoWay=��� #���  #���  $	��220��$��0q�$
��10 0q�$
��Left=�$��Bottom=�$��2��$��3������ *ApplyPasswordCommand+