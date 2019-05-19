export class SortSession {
  id: string; 
  participant: string; 
  sessionData: Date;
  project: Project;
}

export class Project {
    id: string; 
    name: string;
    author: string;
    creationDate: Date;
    defaultGroups: Group[];
    unsortedGroup: Group;
  }
  
  export class Group {
    id: string;
    title: string;
    description: string;
    cards: Card[];

    static getDefault() {
      var newGroup = new Group();
      newGroup.id = Guid.newGuid();
      newGroup.title = 'untitled-group';
      newGroup.description = 'This is yet another sample group.';
      return newGroup;
    }
  }
  
  export class Card {
    id: string;
    title: string;
    description: string;
    order: number;

    static getDefault() {
      var newCard = new Card();
      newCard.id = Guid.newGuid();
      newCard.title = 'untitled-card';
      newCard.description = 'This is a sample card.';
      newCard.order = 0;
      return newCard;
    }
  }

 export class Guid {
    static newGuid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
            var r = Math.random()*16|0, v = c == 'x' ? r : (r&0x3|0x8);
            return v.toString(16);
        });
    }
}