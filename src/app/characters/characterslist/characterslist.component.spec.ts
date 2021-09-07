import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CharacterslistComponent } from './characterslist.component';

describe('CharacterslistComponent', () => {
  let component: CharacterslistComponent;
  let fixture: ComponentFixture<CharacterslistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CharacterslistComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CharacterslistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
