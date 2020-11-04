
import { AppComponent } from "./app.component";
import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./components/home/home.component";
import { BooksListComponent } from "./components/books/books-list/books-list.component";
import { BookDetailsComponent } from "./components/books/book-details/book-details.component";
import { LoginComponent } from "./components/user/login/login.component";
import { MapsComponent } from "./components/maps/maps.component";
import { UsersBasketBooksComponent } from "./components/user/users-basket-books/users-basket-books.component";
import { UserAccountComponent } from "./components/user/user-account/user-account.component";
import { AboutTheSiteComponent } from "./components/about-the-site/about-the-site.component";


export const router: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'books'
  },

  
  { path: "home", component: HomeComponent },
  { path: "books", component: BooksListComponent },
  { path: "oneBook", component: BookDetailsComponent },
  { path: "login", component: LoginComponent },
  { path: "map", component: MapsComponent },
  { path: "userDetails", component: UserAccountComponent },
  { path: "aboutTheSite", component: AboutTheSiteComponent },
  // {
  //   path: 'user', 
  //   children: [ ]
  // }
  // {
  //   path: 'shop',
  //  

  //   //   { path: '', component: MainComponent },
  //   //   { path: 'products/:categoryCode', component: ProductComponent },
  //   //   { path: 'parts/:categoryCode', component: PartsComponent },
  //   //   { path: 'information', component: InformationComponent },
  //   //   { path: 'recommenders', component: RecommendersComponent },
  //   //   { path: 'login', component: LoginComponent },
  //   //   { path: 'register/:update', component: RegisterComponent },
  //   //   { path: 'update_details', component: UpdateDetailsComponent },
  //   //   { path: 'productDetails/:id', component: ProductDetailsComponent },
  //   //   { path: 'partDetails/:id', component: PartDetailsComponent },
  //   //   { path: 'cart', component: CartComponent },
  //   //   { path: 'order', component: OrderComponent },
  //   //   { path: 'oldOrders/:customerEmail', component: OldOrdersComponent },

  //     {
  //       path: 'admin', children: [
  //       //   { path: '', component: AdminComponent },
  //       //   { path: 'productsAdmin/:id', component: ProductsAdminComponent },
  //       //   { path: 'partsAdmin/:id', component: PartsAdminComponent },
  //       //   { path: 'ordersAdmin', component: OrdersAdminComponent },
  //       //   { path: 'customersAdmin', component: CustomersAdminComponent },
  //       //   { path: 'minimum', component: MinimumComponent },
  //       //   { path: 'productDetailsAdmin/:id', component: ProductDetailsAdminComponent },
  //       ]
  //     },
  //   ]
  // },
  {
    path: '**', redirectTo: 'home'
  }
]




