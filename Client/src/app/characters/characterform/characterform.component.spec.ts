import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CharacterformComponent } from './characterform.component';

describe('CharacterformComponent', () => {
  let component: CharacterformComponent;
  let fixture: ComponentFixture<CharacterformComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CharacterformComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CharacterformComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
