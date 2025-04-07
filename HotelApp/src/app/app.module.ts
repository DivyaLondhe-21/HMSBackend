import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NgModule } from '@angular/core';

@NgModule({
  declarations: [/* Your components */],
  imports: [HttpClientModule, /* Other modules */],
  bootstrap: [AppComponent]
})
export class AppModule { }
