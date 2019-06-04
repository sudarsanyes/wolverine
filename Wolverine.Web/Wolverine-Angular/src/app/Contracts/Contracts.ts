export class SortResult {
  projectId: string;
  referencedProject: Project;
  cardGroupResolvedMap: Map<Card, GroupMap[]>;
}

export class SortSessionInfo{
  id: string; 
  participant: string;
}

export class GroupMap {
  session: SortSessionInfo;
  group: Group;
}

export class SortSession {
  id: string;
  participant: string;
  sessionInstance: Date;
  reference: string;
  comments: string;
  project: Project;
}

export class SimplifiedProject {
  id: string;
  name: string;
  author: string;
  creationDate: Date;
  isSessionZero: Boolean;
  defaultGroups: Group[];
  unsortedGroup: Group;
  sessions: SortSession[];
  isLocked: boolean;
  isPublished: boolean;

  constructor() {
    // Reserved. 
  }
}

export class Project {
  id: string;
  name: string;
  author: string;
  creationDate: Date;
  isSessionZero: Boolean;
  groups: Group[];
  sessions: SortSession[];
  isLocked: boolean;
  isPublished: boolean;
}

export class Group {
  id: string;
  title: string;
  description: string;
  isUnsorted: boolean;
  cards: Card[];
  reference: string;

  static getDefault() {
    var newGroup = new Group();
    newGroup.id = Guid.newGuid();
    newGroup.title = 'untitled-group';
    newGroup.description = 'This is yet another sample group.';
    newGroup.cards = [];
    return newGroup;
  }
}

export class Card {
  id: string;
  title: string;
  description: string;
  order: number;
  reference: string;

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
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }
}

export class Helpers {
  static toProject(simplifiedProject: SimplifiedProject): Project {
    var asProject = new Project();
    asProject.id = simplifiedProject.id;
    asProject.name = simplifiedProject.name;
    asProject.author = simplifiedProject.author;
    asProject.creationDate = simplifiedProject.creationDate;
    asProject.isSessionZero = simplifiedProject.isSessionZero;
    asProject.sessions = simplifiedProject.sessions;
    asProject.isLocked = simplifiedProject.isLocked;
    asProject.isPublished = simplifiedProject.isPublished;
    asProject.groups = [];
    asProject.groups.push(simplifiedProject.unsortedGroup);
    for (let group of simplifiedProject.defaultGroups) {
      asProject.groups.push(group);
    }
    return asProject;
  }
}