# Promotions
Single item and multi item promotions

I chose problem statement 1

1) Setup project structure (clean architecture principles applied)
2) Assembly projects were setup as .NET Standard so that they can be used with .Net Framework and .Net Core projects. 
3) All Dependencies were NEWED UP at composition root (Console App). These can easily be moved to IOC container. (Beyond the scope of this exercise)(StructureMap)
4) Favoured Composition Over inheritance for setting up the promotion classes. Inheritance didnt offer any real benefits in this scenario. 
5) The Domain classes arent implemented fully. Just enough to get things working. 
6) The MenuItem abstraction has discountPrice as well as a full price. I dont like this. There is a missing abstraction which would reveal it self when developing the solution further. 
7) Being 100% honest i dont practice TDD all the time in my current role. Its a very strict discipline to follow. I use it where applicable.
8) Promotions order and importance can be controlled inside the promotionEngine class. I have done the implementation so that the promotions are applied in the order they 
are added to the PromotionEngine. I would suspect you would have some business logic like ProductLevel promotions are more important that category and department level promotions.
9) If you run the console app it will generate output for SCENARIO A and SCENARIO B in your requirements document. The code in the console app hasnt been refactored. More work is required here. 
10) The Basket and PromotionEngine Classes need more unit tests. I simply ran out of time based on the criteria in your requirements document. I think i spent about 3 hours on this task. The requirement was 1.5 hours so i did not want to over develop the requirements. 
11) The promotionEngine and promotion classes should be in their own project. However I kept things simple and left them in the domain project.
12) I used the DiscountPrice to establish if a promotion is already applied. This is not great but it got things working. Realistically the promotions should return a newType which describe exactly what items have had which promotion applied and there should be a concept of SavingsMade which should be refelected on the reciept. This is all the beyond the scope of this exercise.
13) To introduce new promotions you simply create a new class inherit from IPromotion and inject it into the PromotionEngine. You can even get your IOC container to do this for you.     
