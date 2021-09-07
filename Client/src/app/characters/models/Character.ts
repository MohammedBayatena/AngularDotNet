import { Skill } from './Skill';
import { Weapon } from './Weapon';
export interface Character {
  id: number;
  name: string;
  hitPoints: number;
  strength: number;
  defence: number;
  intelligence: number;
  user: { username: string };
  weapon: Weapon;
  skills: [Skill];
  type: string;
}
